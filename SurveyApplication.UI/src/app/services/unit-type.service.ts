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

  Delete(id: any): Observable<any> {
    const url = `${environment.apiUrl}/LoaiHinhDonVi/DeleteLoaiHinhDonVi/${id}`;
    return this.http.delete(url);
  }
}
