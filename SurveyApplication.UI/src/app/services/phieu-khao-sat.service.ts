import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs';

import {
  SurveyConfig,
  GeneralInfo,
  SaveSurvey,
  BaseCommandResponse,
  HanhChinhVn,
  CreateBaoCaoCauHoiCommand,
} from '@app/models';
import { environment } from '@environments/environment';
import Utils from '@app/helpers/utils';

@Injectable({
  providedIn: 'root',
})
export class PhieuKhaoSatService {
  constructor(private http: HttpClient) {}

  getSurveyConfig(data: string | undefined) {
    let query = Utils.getParamsQuery(['data'], [data ?? '']);
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

  getTinh() {
    return this.http
      .get<HanhChinhVn[]>(`${environment.apiUrl}/PhieuKhaoSat/GetTinh`)
      .pipe(first());
  }

  getQuanHuyen(idTinh: string) {
    return this.http
      .get<HanhChinhVn[]>(
        `${environment.apiUrl}/PhieuKhaoSat/GetQuanHuyen?idTinh=${idTinh}`
      )
      .pipe(first());
  }

  getPhuongXa(idQuanHuyen: string) {
    return this.http
      .get<HanhChinhVn[]>(
        `${environment.apiUrl}/PhieuKhaoSat/GetPhuongXa?idQuanHuyen=${idQuanHuyen}`
      )
      .pipe(first());
  }

  saveSurvey(result: SaveSurvey) {
    return this.http.post<BaseCommandResponse>(
      `${environment.apiUrl}/PhieuKhaoSat/SavePhieuKhaoSat`,
      result
    );
  }

  dongBoBaoCaoCauHoi(data: CreateBaoCaoCauHoiCommand) {
    return this.http.post<BaseCommandResponse>(
      `${environment.apiUrl}/PhieuKhaoSat/DongBoBaoCaoCauHoi`,
      data
    );
  }
}
