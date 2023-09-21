import { Injectable } from '@angular/core';
import { Module } from '@app/models';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ModuleService extends BaseService<Module> {
  constructor(private http: HttpClient) {
    super(http, `${environment.fakeApiUrl}/Module`);
  }
}
