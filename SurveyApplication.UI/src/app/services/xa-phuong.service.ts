import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { HanhChinhVn, XaPhuong } from '@app/models';
import { first } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class XaPhuongService extends BaseService<XaPhuong> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/XaPhuong`);
  }

  getPhuongXaByQuanHuyen(idQuanHuyen: string) {
    return this.http
      .get<HanhChinhVn[]>(
        `${environment.apiUrl}/XaPhuong/GetByQuanHuyen?id=${idQuanHuyen}`
      )
      .pipe(first());
  }

  Import(file: any) {
    return this.http.post<any>(
      `${environment.apiUrl}/XaPhuong/Import`, file);
  }
}
