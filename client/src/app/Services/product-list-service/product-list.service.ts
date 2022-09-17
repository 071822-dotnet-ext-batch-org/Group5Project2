import { Orders } from '../../Models/Orders';


import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Product } from '../../Models/Product';
import { baseURL } from '../base-url';


@Injectable({
  providedIn: 'root'
})
export class ProductListService {

  constructor(private http: HttpClient) { }

  //Display all products
  public getProducts(): Observable<Product[]> {

    return this.http.get<Product[]>(baseURL + '/products')

  }

   
  //Display orders 
  public getOrders(): Observable<Orders[]> {
    
    return this.http.get<Orders[]>(`${baseURL}/my-orders`)
    
  }
  


  //Display product orders by ID
  public getOrdersById(orderID : any): Observable<any> {
    
    return this.http.get<any>(`${baseURL}/my-orders/` + orderID)
    
  }

  
}
