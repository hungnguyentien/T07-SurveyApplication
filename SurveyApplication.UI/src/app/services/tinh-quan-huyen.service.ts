import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TinhQuanHuyenService {

  constructor(private http: HttpClient) {
   
  }

  GetAllTinh() {
    const url = `${environment.apiUrl}/PhieuKhaoSat/GetTinh`;
    return this.http.get(url);
  }

  GetAllHuyen() {
    const url = `${environment.apiUrl}/PhieuKhaoSat/GetAllQuanHuyen`;
    return this.http.get(url);
  }
  GetAllXa(id:any) {
    const url = `${environment.apiUrl}/PhieuKhaoSat/GetPhuongXa`;
    return this.http.get(url,id);
  }
}
