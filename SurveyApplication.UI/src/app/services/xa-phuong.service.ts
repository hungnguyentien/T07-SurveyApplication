import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { XaPhuong } from '@app/models';

@Injectable({
  providedIn: 'root'
})
export class XaPhuongService extends BaseService<XaPhuong> {
  constructor(private http: HttpClient) {
    super(http, `${environment.apiUrl}/XaPhuong`);
  }
}
