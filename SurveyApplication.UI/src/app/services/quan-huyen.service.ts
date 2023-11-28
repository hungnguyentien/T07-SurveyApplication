import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { HanhChinhVn, QuanHuyen } from '@app/models';
import { first } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuanHuyenService extends BaseService<QuanHuyen> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/QuanHuyen`);
  }

  getQuanHuyenByTinhTp(idTinhTp: string) {
    return this.http
      .get<HanhChinhVn[]>(
        `${environment.apiUrl}/QuanHuyen/GetByTinhTp?id=${idTinhTp}`
      )
      .pipe(first());
  }

  Import(file: any) {
    return this.http.post<any>(
      `${environment.apiUrl}/QuanHuyen/Import`, file);
  }
}
