import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseQuerieResponse, Paging, TableSurvey } from '@app/models';
import { environment } from '@environments/environment';
import { Observable, first } from 'rxjs';
import { BaseService } from './base.service';
import Utils from '@app/helpers/utils';

@Injectable({
  providedIn: 'root',
})
export class TableSurveyService extends BaseService<TableSurvey> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/BangKhaoSat`);
  }

  getBangKhaoSatByDotKhaoSat(code: number): Observable<BaseQuerieResponse<TableSurvey>> {
    // Sử dụng code ở đây để truy vấn dữ liệu
    const params = new HttpParams().set('code', code.toString()); // Chuyển số nguyên thành chuỗi
  
    return this._http
      .get<BaseQuerieResponse<TableSurvey>>(`${this.actionUrl}/GetByDotKhaoSat`, { params })
      .pipe(first());
  }
  

  generateMaBangKhaoSat() {
    const url = `${environment.apiUrl}/BangKhaoSat/GenerateMaBangKhaoSat`;
    return this.http.get(url);
  }
  
  // SearchTableSurvey(pageIndex: number, pageSize: number, keyword: string) {
  //   const url = `${environment.apiUrl}/BangKhaoSat/GetBangKhaoSatByCondition`;
  //   const params = {
  //     pageIndex: pageIndex.toString(),
  //     pageSize: pageSize.toString(),
  //     keyword: keyword,
  //   };
  //   return this.http.get(url, { params });
  // }

  // Insert(model: TableSurvey) {
  //   const url = `${environment.apiUrl}/BangKhaoSat/CreateBangKhaoSat`;
  //   return this.http.post(url, model);
  // }
  // Update(model: TableSurvey) {
  //   const url = `${environment.apiUrl}/BangKhaoSat/UpdateBangKhaoSat`;
  //   return this.http.post(url, model);
  // }

  // Delete(id: any): Observable<any> {
  //   const url = `${environment.apiUrl}/BangKhaoSat/DeleteBangKhaoSat/${id}`;
  //   return this.http.delete(url);
  // }

  // GetAllUnitType(): Observable<any[]> {
  //   const url = `${environment.apiUrl}/LoaiHinhDonVi/GetAllLoaiHinhDonVi`;
  //   return this.http.get<any[]>(url);
  // }
  // GetAllPeriodSurvey(): Observable<any[]> {
  //   const url = `${environment.apiUrl}/DotKhaoSat/GetAllDotKhaoSat`;
  //   return this.http.get<any[]>(url);
  // }
}
