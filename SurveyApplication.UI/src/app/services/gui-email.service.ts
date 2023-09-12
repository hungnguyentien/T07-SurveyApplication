import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { BaseCommandResponse, CreateGuiEmail, GuiEmail } from '@app/models';
import { Observable, first } from 'rxjs';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class GuiEmailService extends BaseService<GuiEmail> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/GuiEmail`);
  }

  createByDonVi(data: CreateGuiEmail): Observable<BaseCommandResponse> {
    return this.http
      .post<BaseCommandResponse>(
        `${environment.apiUrl}/GuiEmail/CreateByDonVi`,
        data
      )
      .pipe(first());
  }
}
