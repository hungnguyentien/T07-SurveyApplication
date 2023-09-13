import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { QuanHuyen } from '@app/models';

@Injectable({
  providedIn: 'root'
})
export class QuanHuyenService extends BaseService<QuanHuyen> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/TinhTp`);
  }
}
