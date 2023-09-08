import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Login } from '../models';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '@app/services/auth.service';

@Injectable({
  providedIn: 'root'
})

export class LoginserviceService {
  private currentUserSubject: BehaviorSubject<string>;
  public currentUser: Observable<string>;

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private authService: AuthService
  ) {
    this.currentUserSubject = new BehaviorSubject<string>(this.cookieService.get('currentUser'));
    this.currentUser = this.currentUserSubject.asObservable();
  }
  
  login(model: Login) {
    debugger
    // model.grant_type = 'password';
    // let body = new URLSearchParams();
    // body.set('Email', model.UserName);
    // body.set('Password', model.Password);
    // body.set('grant_type', model.grant_type);
    const loginData = {
      Email: model.UserName,
      Password: model.Password,
      grant_type: 'password'
    };
    let options = {
      // headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
      headers: new HttpHeaders().set('Content-Type', 'application/json')
    };
    return this.http.post(`${environment.apiUrl}`+ '/Account/login', loginData, options)
      .pipe(map(req => {
        debugger
        // đăng nhập thành công lưu lại token
        if (req) {
          // Xóa hết cookie
          this.cookieService.delete('currentUser');
          // lưu token vào Cookie
          this.cookieService.set('currentUser', JSON.stringify(req));
          this.currentUserSubject.next(JSON.stringify(req));
          this.authService.login();
        }
        return req;
      }));
  }
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
