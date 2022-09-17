import { OrderID } from './../../Models/OrderID';


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

   
  //Get previous orders by user ID
  public getPriorOrdersByUserID(userOrderID: string): Observable<string> {
    
    return this.http.get<string>(`${baseURL}/my-orders?userID=` + userOrderID)
    
  }
  
  //Display order by order ID
  public getOrdersById(orderID : string): Observable<string> {

    return this.http.get<string>(baseURL + '/my-orders' + orderID)

  }

  
}
