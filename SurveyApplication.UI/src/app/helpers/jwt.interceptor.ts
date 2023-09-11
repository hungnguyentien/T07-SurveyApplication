import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';
import { LoginService } from '../services/login.service';
@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private loginService: LoginService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    const isApiUrl = request.url.startsWith(environment.apiUrl);
    const currentUser: any =
      this.currentUserValue() && JSON.parse(this.currentUserValue());
    if (currentUser && isApiUrl) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser}`,
        },
      });
    }

    return next.handle(request);
  }

  currentUserValue(): string {
    return this.loginService.currentUserValue();
  }
}
