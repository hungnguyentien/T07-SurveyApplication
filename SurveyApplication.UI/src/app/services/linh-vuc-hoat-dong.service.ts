import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { LinhVucHoatDong } from '@app/models';

import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LinhVucHoatDongService extends BaseService<LinhVucHoatDong> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/LinhVucHoatDong`);
  }

  generateMaLinhVuc() {
    const url = `${environment.apiUrl}/LinhVucHoatDong/GenerateMaLinhVuc`;
    return this.http.get(url);
  }
}
