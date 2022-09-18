import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataSharingService {

  constructor() { }

  private cartItems: Subject<number> = new Subject<number>();
  private checkoutTotal: Subject<number> = new Subject<number>();

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
}
