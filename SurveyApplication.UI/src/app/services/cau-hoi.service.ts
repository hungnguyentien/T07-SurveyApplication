import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { CauHoi, Select } from '@app/models';
import { environment } from '@environments/environment';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class CauHoiService extends BaseService<CauHoi> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/CauHoi`);
  }

  getLoaiCauHoi() {
    return this.http.get<Select[]>(
      `${environment.apiUrl}/CauHoi/GetAllLoaiCauHoi`
    );
  }
}
