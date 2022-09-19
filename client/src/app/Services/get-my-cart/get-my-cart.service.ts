import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Checkout } from 'src/app/Models/Checkout';
import { baseURL } from '../base-url';

@Injectable({
  providedIn: 'root'
})
export class GetMyCartService {

  constructor(private http: HttpClient) { }

  public getMyCart(): Observable<Checkout> {
    return this.http.get<Checkout>(baseURL+ '/my-cart');
  }
}
