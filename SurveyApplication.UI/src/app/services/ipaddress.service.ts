import { Injectable } from '@angular/core';
import { IpAddress } from '@app/models';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root',
})
export class IpaddressService extends BaseService<IpAddress> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/IpAddress`);
  }
}
