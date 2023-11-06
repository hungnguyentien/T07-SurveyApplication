import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateUnitAndRep } from '@app/models/CreateUnitAndRep';
import { environment } from '@environments/environment';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { BaseCommandResponse } from '@app/models';

@Injectable({
  providedIn: 'root',
})
export class ObjectSurveyService extends BaseService<CreateUnitAndRep> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/DonVi`);
  }

  GetAllFieldOfActivity(): Observable<any[]> {
    const url = `${environment.apiUrl}/LinhVucHoatDong/GetAll`;
    return this.http.get<any[]>(url);
  }

  Import(file: any) {
    return this.http.post<any>(
      `${environment.apiUrl}/DonVi/Import`, file);
  }
}
