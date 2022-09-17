import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserInfo } from '../../Models/UserInfo';
import { baseURL } from '../base-url';

@Injectable({
  providedIn: 'root'
})
export class GetUserInfoService {

  constructor(private http: HttpClient) { }

  public getUserInfo(): Observable<UserInfo> {
    return this.http.get<UserInfo>(baseURL+ '/user');
  }
}
