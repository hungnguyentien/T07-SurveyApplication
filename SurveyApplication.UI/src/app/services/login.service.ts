import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Login } from '../models';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})

export class LoginserviceService {
  private currentUserSubject: BehaviorSubject<string>;
  public currentUser: Observable<string>;

  constructor(
    private http: HttpClient,
    private cookieService: CookieService
  ) {
    this.currentUserSubject = new BehaviorSubject<string>(this.cookieService.get('currentUser'));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(model: Login) {
    model.grant_type = 'password';
    let body = new URLSearchParams();
    body.set('username', model.UserName);
    body.set('password', model.Password);
    body.set('grant_type', model.grant_type);
   
    let options = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };
    return this.http.post(`${environment.apiUrl}`, body.toString(), options)
      .pipe(map(req => {
        // đăng nhập thành công lưu lại token
        if (req) {
          // Xóa hết cookie
          this.cookieService.delete('currentUser');
          // lưu token vào Cookie
          this.cookieService.set('currentUser', JSON.stringify(req));
          this.currentUserSubject.next(JSON.stringify(req));
        }

        return req;
      }));
  }

  // Lấy token trong localStorage value currentUserHrm
  currentUserValue(): string {
    return this.currentUserSubject.value;
  }

  getRoleUser() {
    const token = this.currentUserValue();
    if (token) {
      return "Admin";
    } 

    return null;
 
  }

  logout() {
    // remove user from local storage to log user out
    this.cookieService.delete('currentUser');
    this.currentUserSubject.next("");
  }
}
