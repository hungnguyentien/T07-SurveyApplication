import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Login } from '../models';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private currentUserSubject: BehaviorSubject<string>;
  public currentUser: Observable<string>;

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private router: Router
  ) {
    this.currentUserSubject = new BehaviorSubject<string>(
      this.cookieService.get('currentUser')
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(model: Login) {
    const loginData = {
      email: model.UserName,
      password: model.Password,
      grant_type: 'password',
    };
    let options = {
      // headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
      headers: new HttpHeaders().set('Content-Type', 'application/json'),
    };
    return this.http
      .post(`${environment.apiUrl}/Account/login`, loginData, options)
      .pipe(
        map((req) => {
          // đăng nhập thành công lưu lại token
          if (req) {
            // Xóa hết cookie
            this.cookieService.delete('currentUser');
            // lưu token vào Cookie
            this.cookieService.set('currentUser', JSON.stringify(req));
            this.currentUserSubject.next(JSON.stringify(req));
          }

          return req;
        })
      );
  }

  currentUserValue(): string {
    return this.currentUserSubject.value;
  }

  getRoleUser() {
    const token = this.currentUserValue();
    if (token) {
      return 'Administrator';
    }
    return null;
  }

  logout() {
    // remove user from local storage to log user out
    this.cookieService.delete('currentUser', '/');
    this.currentUserSubject.next('');
    this.router.navigate(['/login']);
  }
}