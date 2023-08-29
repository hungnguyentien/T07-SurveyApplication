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

  Insert(url: string,model: any): Observable<any>{
    return this.http.post(`${environment.apiUrl}${url}`, model).pipe(
    );
  }
  GetById(url: string,id: any): Observable<any>{
    return this.http.get<any>(`${environment.apiUrl}${url}/${id}`).pipe();
  }

  Update(url: string, model: any): Observable<any>{
    return this.http.post(`${environment.apiUrl}${url}`, model).pipe();
  }

  Delete(url: string, model: string[]): Observable<any>{
    return this.http.post(`${environment.apiUrl}${url}`, model).pipe();
  }
  Search(url : string,fromdata : object):Observable<any>{
    return this.http.post<UnitType[]>(`${environment.apiUrl}${url}`, fromdata).pipe();
  }
}
