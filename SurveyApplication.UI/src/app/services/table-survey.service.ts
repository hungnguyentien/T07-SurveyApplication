import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TableSurvey } from '@app/models';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TableSurveyService {
  constructor(private http: HttpClient) {}
  SearchTableSurvey(pageIndex: number, pageSize: number, keyword: string) {
    const url = `${environment.apiUrl}/LoaiHinhDonVi/GetLoaiHinhDonViByCondition`;
    const params = {
      pageIndex: pageIndex.toString(),
      pageSize: pageSize.toString(),
      keyword: keyword
    };
    return this.http.get(url, { params });
  }

  Insert(model: TableSurvey){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/CreateLoaiHinhDonVi`;
    return this.http.post(url,model);
  }
  Update(model: TableSurvey){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/UpdateLoaiHinhDonVi`;
    return this.http.post(url,model);
  }
  GetIdUnitType(){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/GetLastRecordByMaLoaiHinh`
    return this.http.get(url);
  }

  Delete(id: any): Observable<any> {
    const url = `${environment.apiUrl}/LoaiHinhDonVi/DeleteLoaiHinhDonVi/${id}`;
    return this.http.delete(url);
  }
}
