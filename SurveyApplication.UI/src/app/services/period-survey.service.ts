import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { PeriodSurvey } from '@app/models';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class PeriodSurveyService extends BaseService<PeriodSurvey> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/DotKhaoSat`);
  }
}
