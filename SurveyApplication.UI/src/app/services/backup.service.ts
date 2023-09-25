import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  BackupRestore,
  BaseCommandResponse,
  ConfigJobBackup,
  Select,
} from '@app/models';
import { BaseService } from './base.service';
import { environment } from '@environments/environment';
import { Observable, first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BackupService extends BaseService<BackupRestore> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/BackupRestore`);
  }

  backupNow(): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${this.actionUrl}/BackupNow`, null)
      .pipe(first());
  }

  restoreData(data: string): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${this.actionUrl}/RestoreData/${data}`, null)
      .pipe(first());
  }

  configBackup(data: ConfigJobBackup): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${this.actionUrl}/ConfigBackup`, data)
      .pipe(first());
  }

  getScheduleDayofweek(): Observable<Select[]> {
    return this.http
      .get<Select[]>(`${this.actionUrl}/ScheduleDayofweek`)
      .pipe(first());
  }

  getScheduleHour(): Observable<Select[]> {
    return this.http
      .get<Select[]>(`${this.actionUrl}/ScheduleHour`)
      .pipe(first());
  }

  getScheduleMinute(): Observable<Select[]> {
    return this.http
      .get<Select[]>(`${this.actionUrl}/ScheduleMinute`)
      .pipe(first());
  }

  getConfigBackup(): Observable<ConfigJobBackup> {
    return this.http
      .get<ConfigJobBackup>(`${this.actionUrl}/GetConfigBackup`)
      .pipe(first());
  }
}
