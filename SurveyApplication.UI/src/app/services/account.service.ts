import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Account } from '@app/models';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AccountService extends BaseService<Account> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/Account`);
  }
}
