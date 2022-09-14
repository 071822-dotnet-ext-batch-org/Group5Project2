import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Product } from '../Models/Product';

@Injectable({
  providedIn: 'root'
})
export class EcommerceAPIService {

  private baseURL = 'https://localhost:7231/Ecommerce';

  constructor(private http: HttpClient) { }

  public getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseURL}/products`)
  }

}
