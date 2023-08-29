import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs';

import { SurveyConfig, GeneralInfo } from '@app/models';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PhieuKhaoSatService {

  constructor(private http: HttpClient) { }

  getSurveyConfig() {
    return this.http.get<SurveyConfig[]>(
      `${environment.apiUrl}/PhieuKhaoSat/GetConfigPhieuKhaoSat?idBangKhaoSat=1`
    ).pipe(first());
  }

  getGeneralInfo(idDonVi: number) {
    return this.http.get<GeneralInfo>(
      `${environment.apiUrl}/PhieuKhaoSat/GetThongTinChung?idDonVi=${idDonVi}`
    ).pipe(first());
  }
}
