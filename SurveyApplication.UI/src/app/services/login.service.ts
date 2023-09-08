import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Login } from '../models';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '@app/services/auth.service';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})

export class LoginserviceService {
  private currentUserSubject: BehaviorSubject<string>;
  public currentUser: Observable<string>;

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private authService: AuthService,
    private messageService: MessageService
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
      grant_type: 'Password'
    };
    let options = {
      // headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
      headers: new HttpHeaders().set('Content-Type', 'application/json')
    };
    return this.http.post(`${environment.apiUrl}`+ '/Account/login', loginData, options)
      .pipe(map((req:any) => {
        debugger
        // đăng nhập thành công lưu lại token
        if (req) {
          // Xóa hết cookie
          this.cookieService.delete('currentUser');
          // lưu token vào Cookie
          this.cookieService.set('currentUser', JSON.stringify(req.token));
          this.currentUserSubject.next(JSON.stringify(req.token));
          this.authService.login();
        }
        return req;
      }),
      catchError(error => {
        // Xử lý lỗi từ server
        if (error.status === 500) {
          this.messageService.add({
            severity: 'error',
            detail: 'Đăng nhập không thành công, mật khẩu hoặc tài khoản không chính xác !',
          });
        } else if (error.status === 400) {
          console.log('Lỗi đăng nhập: ' + error.error);
        }
        return throwError(error);
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
