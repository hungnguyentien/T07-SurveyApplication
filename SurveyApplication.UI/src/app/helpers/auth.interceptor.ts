import { Injectable } from '@angular/core';
import {
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { LoginService } from '../services/login.service';
import { MessageService } from 'primeng/api';
import Utils from './utils';

@Injectable({ providedIn: 'root' })
export class AuthGuard {
  constructor(
    private router: Router,
    private loginService: LoginService,
    private messageService: MessageService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentUserRole = this.loginService.getRoleUser();
    if (currentUserRole) {
      // kiểm tra quyền có được truy xuất vào trang không
      if (
        route.data['roles'] &&
        route.data['roles'].indexOf(currentUserRole) === -1
      ) {
        // nếu không có quyền quay về trang chủ
        Utils.messageError(
          this.messageService,
          'Bạn không có quyền vào trang!'
        );

        this.router.navigate(['/']);
        return false; // Mặc định là false
      }
      // nếu có quyền trả về true
      return true;
    }

    // chưa đăng nhập thì chuyển hướng đến trang đăng nhập
    this.router.navigate(['/']);
    return false;
  }
}
