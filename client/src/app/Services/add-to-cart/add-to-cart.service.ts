import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { baseURL } from '../base-url';

@Injectable({
  providedIn: 'root'
})
export class AddToCartService {

  constructor(private http: HttpClient) { }

  public addToCart(productID: string): Observable<boolean> {
    return this.http.post<boolean>(baseURL+'/my-cart/addItem?productID='+productID, {})
  }
}
