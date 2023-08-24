import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { LoginserviceService } from '../services/login.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private loginserviceService: LoginserviceService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        let error = err.error || err.statusText;
        if (err.status === 401) {
          error = 'Bạn không có quyền';
          this.loginserviceService.logout();
        } else if (err.status === 0) {
          error = 'Sever không hoạt động';
        }

        // Xóa Console log
        console.clear();

        return throwError(error);
      })
    );
  }
}
