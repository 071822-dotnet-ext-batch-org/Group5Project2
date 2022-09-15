import { Register } from './../Models/Register';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Product } from '../Models/Product';

@Injectable({
  providedIn: 'root'
})
export class EcommerceAPIService {

  private AllProductAPI = 'https://localhost:7231'
  private baseURL = 'https://localhost:7231/Ecommerce';

  constructor(private http: HttpClient) { }

  //Display all products
  public getProducts(): Observable<Product[]> {
    // return this.http.get<Product[]>(`${this.baseURL}/products`)

    return this.http.get<Product[]>(this.AllProductAPI + "/FrontStore/GetAllProductsAsync");
  }


  //Register new users
  public postRegistration(data: Register): Observable<Register> {

    return this.http.post<Register>(`${this.baseURL}/register`, data)

  }

  


}
