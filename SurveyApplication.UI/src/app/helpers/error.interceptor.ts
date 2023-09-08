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

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private loginserviceService: LoginService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        let message = err.error || err.statusText;
        if (err.status === 401) {
          message = 'Bạn không có quyền';
          this.loginserviceService.logout();
        } else if (err.status === 0) {
          message = 'Sever không hoạt động';
        }

        console.log(message);
        // Xóa Console log
        environment.production && console.clear();

        return throwError(err);
      })
    );
  }
}
