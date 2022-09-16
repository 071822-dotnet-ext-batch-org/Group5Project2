import { MyOrdersComponent } from './../my-orders/my-orders.component';
import { Register } from './../Models/Register';
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

  //Display all products
  public getProducts(): Observable<Product[]> {

    return this.http.get<Product[]>(`${this.baseURL}/products`)

  }


  //Register new users
  public postRegistration(data: Register): Observable<Register> {

    return this.http.post<Register>(`${this.baseURL}/register`, data)

  }
   
  //Get previous orders by user ID
  public getPriorOrdersByUserID(userOrderID: string): Observable<string> {
    
    return this.http.get<string>(`${this.baseURL}/my-orders?userID=` + userOrderID)
    
  }
  

}
