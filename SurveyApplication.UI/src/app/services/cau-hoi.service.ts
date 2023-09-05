import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs';

import { CauHoi, BaseQuerieResponse, Paging } from '@app/models';
import { environment } from '@environments/environment';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class CauHoiService extends BaseService<CauHoi> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/CauHoi`);
  }
}
