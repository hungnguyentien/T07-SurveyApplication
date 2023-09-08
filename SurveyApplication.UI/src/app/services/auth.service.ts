import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn: boolean = false;

  constructor(private cookieService: CookieService) {
    // Kiểm tra cookie khi ứng dụng khởi động
    this.checkCookie();
  }

  // Kiểm tra xem cookie đã tồn tại hay không
  private checkCookie() {
    this.loggedIn = this.cookieService.check('currentUser');
  }

  // Xác định xem người dùng đã đăng nhập hay chưa
  isLoggedIn(): boolean {
    return this.loggedIn;
  }

  // Đăng nhập thành công
  login(): void {
    this.loggedIn = true;
  }

  logout(): void {
    debugger
    this.loggedIn = false;
    // Xóa cookie khi đăng xuất
    this.cookieService.delete('currentUser');
    this.cookieService.deleteAll();
  }
  
}
