import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { TinhThanh } from '@app/models/TinhThanh';

@Injectable({
  providedIn: 'root'
})
export class TinhThanhService extends BaseService<TinhThanh> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/TinhTp`);
  }

  Import(file: any) {
    return this.http.post<any>(
      `${environment.apiUrl}/TinhTp/Import`, file);
  }
}
