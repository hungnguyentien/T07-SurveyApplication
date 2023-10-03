import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseCommandResponse, CreateUpdateRole, MatrixPermission, Role } from '@app/models';
import { BaseService } from './base.service';
import { environment } from '@environments/environment';
import { first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RoleService extends BaseService<Role> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/Role`);
  }

  getMatrixPermission() {
    return this.http.get<MatrixPermission[]>(
      `${environment.apiUrl}/Role/GetMatrixPermission`
    );
  }

  getPermissionById(id: string) {
    return this.http.get<CreateUpdateRole>(
      `${environment.apiUrl}/Role/GetById/${id}`
    );
  }
  deletePermissionMultiple(ids: string[]) {
    return this._http
      .request<BaseCommandResponse>(
        'delete',
        `${this.actionUrl}/DeleteMultiple`,
        { body: ids }
      )
      .pipe(first());
  }

  
}
