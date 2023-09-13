import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { UnitType } from '@app/models';
import { BaseService } from './base.service';
@Injectable({
  providedIn: 'root',
})
export class UnitTypeService extends BaseService<UnitType> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/LoaiHinhDonVi`);
  }

  generateMaLoaiHinh() {
    const url = `${environment.apiUrl}/LoaiHinhDonVi/GenerateMaLoaiHinhDonVi`;
    return this.http.get(url);
  }
}
