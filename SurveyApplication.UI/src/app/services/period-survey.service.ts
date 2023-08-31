import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { PeriodSurvey } from '@app/models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PeriodSurveyService {
 
  constructor(private http : HttpClient ) { }

  SearchPeriodSurvey(pageIndex: number, pageSize: number, keyword: string) {
    const url = `${environment.apiUrl}/DotKhaoSat/GetDotKhaoSatByCondition`;
    const params = {
      pageIndex: pageIndex.toString(),
      pageSize: pageSize.toString(),
      keyword: keyword
    };
    return this.http.get(url, { params });
  }

  Insert(model: PeriodSurvey){
    const url = `${environment.apiUrl}/DotKhaoSat/CreateDotKhaoSat`;
    return this.http.post(url,model);
  }
  
  Update(model: PeriodSurvey){
    const url = `${environment.apiUrl}/DotKhaoSat/UpdateDotKhaoSat`;
    return this.http.post(url,model);
  }
  

  Delete(id: any): Observable<any> {
    const url = `${environment.apiUrl}/DotKhaoSat/DeleteDotKhaoSat/${id}`;
    return this.http.delete(url);
  }
 

  GetAllUnitType(): Observable<any[]> {
    const url = `${environment.apiUrl}/LoaiHinhDonVi/GetAllLoaiHinhDonVi`; 
    return this.http.get<any[]>(url);
  }
  }
