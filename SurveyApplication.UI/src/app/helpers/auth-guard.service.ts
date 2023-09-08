import { Injectable } from '@angular/core';
import { AuthService } from '@app/services/auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(): boolean {
    debugger
    if (this.authService.isLoggedIn()) {
      return true;
    } else {
      // Nếu chưa đăng nhập, chuyển hướng về trang đăng nhập
      this.router.navigate(['/login']);
      return false;
    }
  }
}
