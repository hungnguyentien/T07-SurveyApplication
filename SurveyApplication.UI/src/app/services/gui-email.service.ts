import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import {
  BaseCommandResponse,
  CreateGuiEmail,
  GuiEmail,
  GuiLaiGuiEmail,
  PagingGuiEmailBks,
  ThuHoiGuiEmail,
} from '@app/models';
import { Observable, first } from 'rxjs';
import { BaseService } from './base.service';
import Utils from '@app/helpers/utils';

@Injectable({
  providedIn: 'root',
})
export class GuiEmailService extends BaseService<GuiEmail> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/GuiEmail`);
  }

  createByDonVi(data: CreateGuiEmail): Observable<BaseCommandResponse> {
    return this.http
      .post<BaseCommandResponse>(
        `${environment.apiUrl}/GuiEmail/CreateByDonVi`,
        data
      )
      .pipe(first());
  }

  guiLaiEmail(data: GuiLaiGuiEmail): Observable<BaseCommandResponse> {
    return this.http
      .post<BaseCommandResponse>(
        `${environment.apiUrl}/GuiEmail/GuiLaiEmail`,
        data
      )
      .pipe(first());
  }

  thuHoiEmail(data: ThuHoiGuiEmail): Observable<BaseCommandResponse> {
    return this.http
      .post<BaseCommandResponse>(
        `${environment.apiUrl}/GuiEmail/ThuHoiEmail`,
        data
      )
      .pipe(first());
  }

  getByIdBangKhaoSat(paging: PagingGuiEmailBks) {
    let query = Utils.getParamsQuery(
      Object.keys(paging),
      Object.values(paging)
    );
    return this._http
      .get(`${environment.apiUrl}/GuiEmail/GetByIdBangKhaoSat${query}`)
      .pipe(first());
  }
}
