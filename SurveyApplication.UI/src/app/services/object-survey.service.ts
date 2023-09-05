import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ObjectSurvey} from '@app/models';
import { CreateUnitAndRep } from '@app/models/CreateUnitAndRep';
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

  Insert(obj: CreateUnitAndRep){
    const url = `${environment.apiUrl}/DonVi/CreateDonVi`;
    return this.http.post(url,obj);
  }


  Update(model: CreateUnitAndRep){
    const url = `${environment.apiUrl}/DonVi/UpdateDonVi`;
    return this.http.post(url,model);
  }



  GetIdObjectSurvey(){
    const url = `${environment.apiUrl}/LoaiHinhDonVi/GetLastRecordByMaLoaiHinh`
    return this.http.get(url);
  }

  // /DonVi/DeleteDonVi?idDonVi=1&idNguoiDaiDien=1
  Delete(idDonVi: number, idNguoiDaiDien: number) {
    const url = `${environment.apiUrl}/DonVi/DeleteDonVi`;
    const params = {
      idDonVi: idDonVi,
      idNguoiDaiDien: idNguoiDaiDien,
    };
    return this.http.delete(url, { params });
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
