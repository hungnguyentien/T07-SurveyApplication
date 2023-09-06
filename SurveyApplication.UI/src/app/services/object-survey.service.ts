import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateUnitAndRep } from '@app/models/CreateUnitAndRep';
import { environment } from '@environments/environment';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class ObjectSurveyService extends BaseService<CreateUnitAndRep> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/DonVi`);
  }
}
