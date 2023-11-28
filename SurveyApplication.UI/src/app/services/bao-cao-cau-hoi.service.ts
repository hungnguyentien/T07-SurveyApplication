import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import {
  BaoCaoCauHoiChiTiet,
  BaoCaoCauHoiChiTietRequest,
  BaoCaoCauHoiRequest,
  BaseQuerieResponse,
  FileQuestion,
  StgFile,
  ThongKeBaoCao,
} from '@app/models';
import { environment } from '@environments/environment';
import Utils from '@app/helpers/utils';
import { Observable, first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BaoCaoCauHoiService {
  constructor(private http: HttpClient) {}
  private sharedData: any;
  getBaoCaoCauHoi(params: BaoCaoCauHoiRequest) {
    let query = Utils.getParamsQuery(
      Object.keys(params),
      Object.values(params)
    );
    return this.http.get<ThongKeBaoCao>(
      `${environment.apiUrl}/BaoCaoCauHoi/GetBaoCaoCauHoi${query}`
    );
  }

  getBaoCaoCauHoiChiTiet(
    paging: BaoCaoCauHoiChiTietRequest
  ): Observable<BaseQuerieResponse<BaoCaoCauHoiChiTiet>> {
    let query = Utils.getParamsQuery(
      Object.keys(paging),
      Object.values(paging)
    );
    return this.http
      .get<BaseQuerieResponse<BaoCaoCauHoiChiTiet>>(
        `${environment.apiUrl}/BaoCaoCauHoi/GetBaoCaoCauHoiChiTiet${query}`
      )
      .pipe(first());
  }

  downloadFile(id: number) {
    return this.http.get<StgFile>(`${environment.apiUrl}/BaoCaoCauHoi/DownloadFile/${id}`)
    .pipe(first());
  }

  setSharedData(data: any) {
    this.sharedData = data;
  }

  getSharedData() {
    return this.sharedData;
  }
}
