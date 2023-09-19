import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Utils from '@app/helpers/utils';
import { BaoCaoCauHoi, DashBoardRequest } from '@app/models';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminHomeService {

  constructor(private http: HttpClient) {}

  GetAllDashBoard(): Observable<any[]>  {
      const url = `${environment.apiUrl}/BaoCaoCauHoi/GetDashBoard`;
      return this.http.get<any[]>(url);
  }
    GetDashBoard(ngayBatDau:string, ngayKetThuc: string) {
      const url = `${environment.apiUrl}/BaoCaoCauHoi/GetDashBoard`;
      const params = {
        NgayBatDau: ngayBatDau,
        NgayKetThuc: ngayKetThuc,
      };
      return this.http.get(url, { params });
    }

}
