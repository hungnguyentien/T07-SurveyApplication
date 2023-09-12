import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FieldOfActivityService {

  constructor(private http: HttpClient) {
   
  }

  GetAll() {
    const url = `${environment.apiUrl}/LinhVucHoatDong/GetAll`;
    return this.http.get(url);
  }
}
