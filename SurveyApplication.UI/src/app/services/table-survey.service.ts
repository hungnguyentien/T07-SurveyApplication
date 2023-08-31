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
    const url = `${environment.apiUrl}/BangKhaoSat/GetBangKhaoSatByCondition`;
    const params = {
      pageIndex: pageIndex.toString(),
      pageSize: pageSize.toString(),
      keyword: keyword
    };
    return this.http.get(url, { params });
  }

  Insert(model: TableSurvey){
    const url = `${environment.apiUrl}/BangKhaoSat/CreateBangKhaoSat`;
    return this.http.post(url,model);
  }
  Update(model: TableSurvey){
    const url = `${environment.apiUrl}/BangKhaoSat/UpdateBangKhaoSat`;
    return this.http.post(url,model);
  }
 

  Delete(id: any): Observable<any> {
    const url = `${environment.apiUrl}/BangKhaoSat/DeleteBangKhaoSat/${id}`;
    return this.http.delete(url);
  }
}
