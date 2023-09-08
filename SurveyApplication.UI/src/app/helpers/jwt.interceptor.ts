import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { LoginserviceService } from '../services/login.service';
@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private loginService: LoginserviceService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    const currentUser: any = JSON.parse(this.currentUserValue());
    if (currentUser) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser?.token}`,
        },
      });
    }

    return next.handle(request);
  }

  currentUserValue(): string {
    return this.loginService.currentUserValue();
  }
}
