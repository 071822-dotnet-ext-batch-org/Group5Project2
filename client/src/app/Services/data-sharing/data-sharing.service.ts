import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Product } from 'src/app/Models/Product';

@Injectable({
  providedIn: 'root'
})
export class DataSharingService {

  constructor() { }

  private cartItems: Subject<number> = new Subject<number>();
  private checkoutTotal: Subject<number> = new Subject<number>();
  private checkoutProducts: Subject<Product[]> = new Subject<Product[]>();

  updateCart(items: number){
    this.cartItems.next(items++);
  }

  getUpdatedCart(): Observable<number>{
    return this.cartItems.asObservable();
  }

  updateCheckoutTotal(newTotal: number){
    this.checkoutTotal.next(newTotal);
  }

  getUpdatedCheckoutTotal(): Observable<number>{
    return this.checkoutTotal.asObservable();
  }

  updateCheckoutProducts(products: Product[]){
    return this.checkoutProducts.next(products)
  }

  getUpdatedCheckoutProducts(): Observable<Product[]>{
    return this.checkoutProducts.asObservable();
  }
}
