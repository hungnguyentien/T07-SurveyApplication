import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Account, BaseCommandResponse } from '@app/models';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Observable, first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService extends BaseService<Account> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/Account`);
  }

  register<T>(data: T): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${this.actionUrl}/register`, data)
      .pipe(first());
  }

  getPermissionById(id: string) {
    return this.http.get<Account>(
      `${environment.apiUrl}/Account/GetById/${id}`
    );
  }
}
