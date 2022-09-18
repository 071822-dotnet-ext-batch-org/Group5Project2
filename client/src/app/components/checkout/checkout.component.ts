import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/Models/Product';
import { CheckoutService } from 'src/app/Services/checkout/checkout.service';
import { DataSharingService } from 'src/app/Services/data-sharing/data-sharing.service';
import { GetMyCartService } from 'src/app/Services/get-my-cart/get-my-cart.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {

  products: Product[] = [];
  checkoutTotal: number = 0;
  orderMessage: any;

  constructor(
    private CHK: CheckoutService,
    private DSS: DataSharingService
  ) { 
    this.DSS.getUpdatedCheckoutTotal().subscribe(newTotal => this.checkoutTotal = newTotal);
  }

  ngOnInit(): void {
    
  }

  checkout(): void {
    this.CHK.checkout().subscribe(data => this.orderMessage=data.message)
    this.checkoutTotal = 0;
    this.products = [];
    this.DSS.updateCart(0);
  }

}
