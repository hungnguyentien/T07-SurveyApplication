import { Injectable } from '@angular/core';
import { LoginService } from '@app/services';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardService {
  constructor(private loginService: LoginService, private router: Router) {}

  canActivate(): boolean {
    if (this.loginService.currentUserValue()) {
      return true;
    } else {
      // Nếu chưa đăng nhập, chuyển hướng về trang đăng nhập
      this.router.navigate(['/login']);
      return false;
    }
  }
}
