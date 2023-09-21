import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Login } from '../models';
import { CookieService } from 'ngx-cookie-service';
import jwt_decode from 'jwt-decode';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root',
})
export class LoginService {
  public currentUser: Observable<string>;
  private currentUserSubject: BehaviorSubject<string>;
  datas!:any;

  constructor(private http: HttpClient, private cookieService: CookieService,private router: Router) {
    this.currentUserSubject = new BehaviorSubject<string>(
      localStorage.getItem('isRememberMe') === 'true'
        ? this.cookieService.get('currentUser')
        : sessionStorage.getItem('currentUser') ?? ''
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(model: Login) {
    let options = {
      headers: new HttpHeaders().set('Content-Type', 'application/json'),
    };
    return this.http
      .post(`${environment.apiUrl}` + '/Account/login', model, options)
      .pipe(
        map((req: any) => {
          // đăng nhập thành công lưu lại token
          debugger
          if (req) {
            this.datas = req.id;
            // Xóa hết cookie
            this.cookieService.delete('currentUser');
            localStorage.setItem('isRememberMe', model.isRememberMe.toString());
            // lưu token vào Cookie
            if (model.isRememberMe) {
              this.cookieService.set('currentUser', req.token);
              this.cookieService.set('IdcurrentUser', req.id);
              sessionStorage.removeItem('currentUser');
            } else {
              sessionStorage.setItem('currentUser', req.token);
              this.cookieService.set('IdcurrentUser', req.id);
              this.cookieService.delete('currentUser', '/');
            }

            this.currentUserSubject.next(req.token);
          }

          return req;
        }),
        catchError((error) => {
          // Xử lý lỗi từ server
          if (error.status === 500) {
            console.log(
              'Đăng nhập không thành công, mật khẩu hoặc tài khoản không chính xác !' +
                error.error
            );
          } else if (error.status === 400) {
            console.log('Lỗi đăng nhập: ' + error.error);
          }

          return throwError(error);
        })
      );
  }
  /**
   * Get token currentUser
   * @returns
   */
  currentUserValue(): string {
    return this.currentUserSubject.value;
  }
  /**
   * Get data currentUser theo token
   * @returns
   */
  getCurrentUser(token: string = ''): any | null {
    token = token ? token : this.currentUserValue();
    if (token) {
      try {
        return jwt_decode(token);
      } catch (e) {
        console.error(e);
        return null;
      }
    }
    return null;
  }

  logout() {
    // remove user from local storage to log user out
    this.cookieService.delete('currentUser', '/');
    sessionStorage.removeItem('currentUser');
    this.cookieService.delete('IdcurrentUser', '/');
    sessionStorage.removeItem('IdcurrentUser');
    this.currentUserSubject.next('');
  
  }
  getByIdUser<T>(id: number): Observable<T> {
    return this.http.get<T>(`${environment.apiUrl}/Account/GetById/${id}`);
  }
  
}
