import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LoginService } from '../services/login.service';
import { environment } from '@environments/environment';
import { MessageService } from 'primeng/api';
import Utils from './utils';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private loginService: LoginService,
    private messageService: MessageService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        let message =
          err.error || err.statusText || err.error.ErrorMessage || err.message;
        if (err.status === 401) {
          message = 'Bạn không có quyền';
          this.loginService.logout();
        } else if (err.status === 0) {
          message = 'Sever không hoạt động';
          environment.production && this.loginService.logout();
        } else if (
          err.status === 403 &&
          this.router.url.indexOf('/admin') >= 0
        ) {
          this.router.navigate(['/admin/error-403']);
        } else {
          if (err.errors)
            Utils.messageError(this.messageService, err.errors.at(0) ?? '');
          else if (err.error)
            Utils.messageError(
              this.messageService,
              err.error.ErrorMessage ?? err.message
            );
          if (err.status === 500 && this.router.url.indexOf('/phieu') >= 0) {
            this.router.navigate(['phieu/error-500'], {
              queryParams: { message: err.error.ErrorMessage ?? err.message },
            });
          }
        }

        console.log(message);
        // Xóa Console log
        environment.production && console.clear();
        return throwError(err);
      })
    );
  }
}
