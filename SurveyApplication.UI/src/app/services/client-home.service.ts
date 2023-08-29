import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { SurveyConfig } from '@app/models';

@Injectable({
  providedIn: 'root',
})
export class ClientHomeService {
  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get(`${environment.apiUrl}/BangKhaoSat/GetAllBangKhaoSat`);
  }

  getSurveyConfig() {
    return this.http.get<SurveyConfig[]>(
      `${environment.apiUrl}/PhieuKhaoSat/GetConfigPhieuKhaoSat?idBangKhaoSat=1`
    );
  }
}
