import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { PeriodSurvey } from '@app/models';
import { BaseService } from './base.service';
import { first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PeriodSurveyService extends BaseService<PeriodSurvey> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/DotKhaoSat`);
  }

  getDotKhaoSatByLoaiHinh(idLoaiHinh: number) {
    return this.http
      .get<PeriodSurvey[]>(
        `${environment.apiUrl}/DotKhaoSat/GetByLoaiHinh?id=${idLoaiHinh}`
      )
      .pipe(first());
  }

  getDotKhaoSatByDotKhaoSat(id: number) {
    return this.http
      .get<PeriodSurvey[]>(
        `${environment.apiUrl}/BangKhaoSat/GetByDotKhaoSat?id=${id}`
      )
      .pipe(first());
  }

   generateMaDotKhaoSat() {
    const url = `${environment.apiUrl}/DotKhaoSat/GenerateMaDotKhaoSat`;
    return this.http.get(url);
  }
}
