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

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private loginService: LoginService,
    private messageService: MessageService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        let message = err.error || err.statusText;
        if (err.status === 401) {
          message = 'Bạn không có quyền';
          this.loginService.logout();
        } else if (err.status === 0) {
          message = 'Sever không hoạt động';
          environment.production && this.loginService.logout();
        }

        console.log(message);
        // Xóa Console log
        environment.production && console.clear();
        if (err.errors)
          Utils.messageError(this.messageService, err.errors.at(0) ?? '');
        else if (err.error)
          Utils.messageError(this.messageService, err.error.ErrorMessage ?? err.message);
        return throwError(err);
      })
    );
  }
}
