import { HttpClient } from '@angular/common/http';
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
    let query = Utils.getParamsQuery(
      Object.keys(paging),
      Object.values(paging)
    );
    return this._http
      .get<BaseQuerieResponse<T>>(`${this.actionUrl}/GetByCondition${query}`)
      .pipe(first());
  }

  create<T>(data: T): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${this.actionUrl}/Create`, data)
      .pipe(first());
  }

  update<T>(data: T): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${this.actionUrl}/Update`, data)
      .pipe(first());
  }

  delete(id: number): Observable<BaseCommandResponse> {
    return this._http
      .delete<BaseCommandResponse>(`${this.actionUrl}/Delete/${id}`)
      .pipe(first());
  }

  deleteMultiple(ids: number[]): Observable<BaseCommandResponse> {
    return this._http
      .request<BaseCommandResponse>(
        'delete',
        `${this.actionUrl}/DeleteMultiple`,
        { body: ids }
      )
      .pipe(first());
  }

  deleteParams(id: number): Observable<BaseCommandResponse> {
    const params = { id: id };
    return this._http
      .delete<BaseCommandResponse>(`${this.actionUrl}/Delete`, {
        params: params,
      })
      .pipe(first());
  }

  getByConditionParams(paging: Paging): Observable<BaseQuerieResponse<T>> {
    let getParams = Utils.getParams(Object.keys(paging), Object.values(paging));
    return this._http
      .get<BaseQuerieResponse<T>>(`${this.actionUrl}/GetByCondition`, {
        params: getParams,
      })
      .pipe(first());
  }
}