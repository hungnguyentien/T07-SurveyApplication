import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BaoCaoCauHoi, BaoCaoCauHoiRequest } from '@app/models';
import { environment } from '@environments/environment';
import Utils from '@app/helpers/utils';

@Injectable({
  providedIn: 'root',
})
export class BaoCaoCauHoiService {
  constructor(private http: HttpClient) {}

  getBaoCaoCauHoi(params: BaoCaoCauHoiRequest) {
    let query = Utils.getParamsQuery(Object.keys(params), Object.values(params));
    return this.http.get<BaoCaoCauHoi>(
      `${environment.apiUrl}/BaoCaoCauHoi/GetBaoCaoCauHoi${query}`
    );
  }

  // getBaoCaoDashboard(params: BaoCaoDashboardRequest) {
  //   let query = Utils.getParamsQuery(Object.keys(params), Object.values(params));
  //   return this.http.get<BaoCaoDashboard>(
  //     `${environment.apiUrl}/BaoCaoCauHoi/GetBaoCaoDashboard${query}`
  //   );
    
  // }

}
