import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

const ACCESS_TOKEN = JSON.parse('%7B%22id%22%3A%228e445865-a24d-4543-a6c6-9443d048cdb9%22%2C%22userName%22%3A%22ha%40gmail.com%22%2C%22email%22%3A%22ha%40gmail.com%22%2C%22token%22%3A%22eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJoYUBnbWFpbC5jb20iLCJqdGkiOiI4MDFlNzg4Yi01OWVlLTQ2MjQtYjU4Ny0xMTdjZjA1YjE5YmIiLCJlbWFpbCI6ImhhQGdtYWlsLmNvbSIsInVpZCI6IjhlNDQ1ODY1LWEyNGQtNDU0My1hNmM2LTk0NDNkMDQ4Y2RiOSIsIkFkbWluIjpbIkdldEFsbERvblZpIiwiR2V0RG9uVmlCeUNvbmRpdGlvbiIsIkdldEJ5RG9uVmkiLCJDcmVhdGVEb25WaSIsIlVwZGF0ZURvblZpIiwiRGVsZXRlRG9uVmkiXSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE2OTQxNjQzMjIsImlzcyI6IlN1cnZleU1hbmFnZW1lbnQiLCJhdWQiOiJTdXJ2ZXlNYW5hZ2VtZW50VXNlciJ9.26PmZgp9IW3zPPSRVwL22CZdjvajaKTwG5HiYG-5Zkg%22%7D');
const REFRESH_TOKEN = 'refresh_token';
@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor(private cookieService: CookieService) { }

  getToken(): any {
    return this.cookieService.get(ACCESS_TOKEN);
  }

  getRefreshToken(): any {
    return localStorage.getItem(REFRESH_TOKEN);
  }

  saveToken(token: string): void {
    localStorage.setItem(ACCESS_TOKEN, token);
  }

  saveRefreshToken(refreshToken: string): void {
    localStorage.setItem(REFRESH_TOKEN, refreshToken);
  }

  removeToken(): void {
    localStorage.removeItem(ACCESS_TOKEN);
  }

  removeRefreshToken(): void {
    localStorage.removeItem(REFRESH_TOKEN);
  }
}
