import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ObjectSurvey } from '@app/models';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ObjectSurveyService {

  constructor(private http: HttpClient) {}

  SearchObjectSurvey(pageIndex: number, pageSize: number, keyword: string) {
    const url = `${environment.apiUrl}/DonVi/GetDonViByCondition`;
    const params = {
      pageIndex: pageIndex.toString(),
      pageSize: pageSize.toString(),
      keyword: keyword
    };
    return this.http.get(url, { params });
  }

  Insert(model: ObjectSurvey){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/CreateLoaiHinhDonVi`;
    return this.http.post(url,model);
  }
  Update(model: ObjectSurvey){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/UpdateLoaiHinhDonVi`;
    return this.http.post(url,model);
  }
  GetIdObjectSurvey(){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/GetLastRecordByMaLoaiHinh`
    return this.http.get(url);
  }

  Delete(id: any): Observable<any> {
    const url = `${environment.apiUrl}/DonVi/DeleteDonVi/${id}`;
    return this.http.delete(url);
  }

  getCities(): Observable<any> {
    const apiUrl = 'https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json';
    return this.http.get(apiUrl);
  }
  GetUnitType(){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/GetAllLoaiHinhDonVi`
    return this.http.get(url);
  }
}
