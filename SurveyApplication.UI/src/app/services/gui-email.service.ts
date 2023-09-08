import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { BaseCommandResponse, CreateGuiEmail } from '@app/models';
import { Observable, first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GuiEmailService {
  constructor(private http: HttpClient) {}

  createByDonVi(data: CreateGuiEmail): Observable<BaseCommandResponse> {
    return this.http
      .post<BaseCommandResponse>(`${environment.apiUrl}/GuiEmail/CreateByDonVi`, data)
      .pipe(first());
  }
}
