import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs';

import {
  SurveyConfig,
  GeneralInfo,
  SaveSurvey,
  BaseCommandResponse,
} from '@app/models';
import { environment } from '@environments/environment';
import Utils from '@app/helpers/utils';

@Injectable({
  providedIn: 'root',
})
export class PhieuKhaoSatService {
  constructor(private http: HttpClient) {}

  getSurveyConfig(
    idBangKhaoSat: number,
    idDonVi: number,
    idNguoiKhaoSat: number
  ) {
    let query = Utils.getParamsQuery(
      ['idBangKhaoSat', 'idDonVi', 'idNguoiKhaoSat'],
      [idBangKhaoSat.toString(), idDonVi.toString(), idNguoiKhaoSat.toString()]
    );
    return this.http
      .get<SurveyConfig>(
        `${environment.apiUrl}/PhieuKhaoSat/GetConfigPhieuKhaoSat${query}`
      )
      .pipe(first());
  }

  getGeneralInfo(data: string) {
    return this.http
      .get<GeneralInfo>(
        `${environment.apiUrl}/PhieuKhaoSat/GetThongTinChung?data=${data}`
      )
      .pipe(first());
  }

  saveSurvey(result: SaveSurvey) {
    return this.http.post<BaseCommandResponse>(
      `${environment.apiUrl}/PhieuKhaoSat/SavePhieuKhaoSat`,
      result
    );
  }
}
