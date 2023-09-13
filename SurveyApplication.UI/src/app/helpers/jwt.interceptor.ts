import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, BehaviorSubject, catchError, throwError } from 'rxjs';
import { LoginserviceService } from '../services/login.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  
  constructor(private router: Router,private loginService: LoginserviceService,private cookieService: CookieService, private messageService: MessageService) {}
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    // add authorization header with jwt token if available
    const currentUser = this.currentUserValue();
    if (currentUser) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser}`,
        },  
      });
    }

    return next.handle(request).pipe(
      catchError((error) => {
        if (error.status === 403) {
          this.router.navigate(['/admin/error-403']);
        }
        throw error;
      })
    );
  }
  currentUserValue(): string {
    return this.loginService.currentUserValue();
  }
}
