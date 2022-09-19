import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { baseURL } from '../base-url';
import { Cart } from 'src/app/Models/Cart';

@Injectable({
  providedIn: 'root'
})
export class AddToCartService {

  constructor(private http: HttpClient) { }

  public addToCart(productID: string): Observable<Cart> {
    return this.http.post<Cart>(baseURL+'/my-cart/addItem?productID='+productID+'&count='+'1', {})
  }

  public addCountToCart(productID: string, count: number): Observable<Cart> {
    return this.http.post<Cart>(baseURL+'/my-cart/addItem?productID='+productID+'&count='+count, {})
  }

  public deleteCountFromCart(productID: string, count: number): Observable<Cart> {
    return this.http.post<Cart>(baseURL+'/my-cart/deleteItem?productID='+productID+'&count='+count, {})
  }
}
