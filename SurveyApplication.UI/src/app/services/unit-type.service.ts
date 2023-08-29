import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { UnitType } from '@app/models'; 
@Injectable({
  providedIn: 'root'
})
export class UnitTypeService {

  constructor(private http: HttpClient) {}

  // getAll() {
  //   return this.http.get(`${environment.apiUrl}/BangKhaoSat/GetAllBangKhaoSat`);
  // }
  GetList(url: string,): Observable<any>{
    return this.http.get(`${environment.apiUrl}${url}`)
  }

  GetById(url: string,id: any): Observable<any>{
    return this.http.get<any>(`${environment.apiUrl}${url}/${id}`).pipe();
  }

  
  Delete(url: string, model: string[]): Observable<any>{
    return this.http.post(`${environment.apiUrl}${url}`, model).pipe();
  }
  Search(url : string,fromdata : object):Observable<any>{
    return this.http.post<UnitType[]>(`${environment.apiUrl}${url}`, fromdata).pipe();
  }

  SearchUnitType(pageIndex: number, pageSize: number, keyword: string) {
    const url = `${environment.apiUrl}/LoaiHinhDonVi/GetLoaiHinhDonViByCondition`;
    const params = {
      pageIndex: pageIndex.toString(),
      pageSize: pageSize.toString(),
      keyword: keyword
    };
    return this.http.get(url, { params });
  }

  Insert(model: UnitType){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/CreateLoaiHinhDonVi`;
    return this.http.post(url,model);
  }
  Update(model: UnitType){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/UpdateLoaiHinhDonVi`;
    return this.http.post(url,model);
  }
  GetIdUnitType(){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/GetLastRecordByMaLoaiHinh`
    return this.http.get(url);
  }
}
