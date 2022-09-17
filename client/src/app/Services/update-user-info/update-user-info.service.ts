import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { baseURL } from '../base-url';
import { Profile } from 'src/app/Models/Profile';

@Injectable({
  providedIn: 'root'
})
export class UpdateUserInfoService {

  constructor(private http: HttpClient) { }

  public updateUserInfo(phone: any, address: any): Observable<Profile>{
    return this.http.put<Profile>(baseURL+'/user', {phone, address})
  }
}
