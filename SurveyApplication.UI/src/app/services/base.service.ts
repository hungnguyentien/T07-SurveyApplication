import { HttpClient, HttpParams } from '@angular/common/http';
import Utils from '@app/helpers/utils';
import { BaseCommandResponse, BaseQuerieResponse, Paging } from '@app/models';
import { Observable, first } from 'rxjs';

export abstract class BaseService<T> {
  constructor(protected _http: HttpClient, protected actionUrl: string) {}

  getAll(): Observable<T[]> {
    return this._http.get<T[]>(`${this.actionUrl}/GetAll`).pipe(first());
  }

  getById(id: number): Observable<T> {
    return this._http.get<T>(`${this.actionUrl}/GetById/${id}`).pipe(first());
  }

  getByCondition(paging: Paging): Observable<BaseQuerieResponse<T>> {
    let params = Utils.getParams(Object.keys(paging), Object.values(paging));
    return this._http
      .get<BaseQuerieResponse<T>>(`${this.actionUrl}/GetByCondition`, {
        params,
      })
      .pipe(first());
  }

  create(data: T): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${this.actionUrl}/Create`, data)
      .pipe(first());
  }

  update(data: T): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${this.actionUrl}/Update`, data)
      .pipe(first());
  }

  delete(id: number): Observable<BaseCommandResponse> {
    let params = new HttpParams();
    params.append('id', id);
    debugger;
    return this._http
      .delete<BaseCommandResponse>(`${this.actionUrl}/Delete`, { params })
      .pipe(first());
  }

  deleteMultiple(ids: number[]): Observable<BaseCommandResponse> {
    const params = {
      ids: ids,
    };
    return this._http
      .delete<BaseCommandResponse>(`${this.actionUrl}/DeleteMultiple`, {
        params,
      })
      .pipe(first());
  }
}
