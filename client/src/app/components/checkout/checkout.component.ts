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
    private GMC: GetMyCartService,
    private CHK: CheckoutService,
    private DSS: DataSharingService
  ) { }

  ngOnInit(): void {
    this.displayCart();
  }

  checkout(): void {
    this.CHK.checkout().subscribe(data => this.orderMessage=data.message)
    this.checkoutTotal = 0;
    this.products = [];
    this.DSS.updateCart(0);
  }

  displayCart(): void {
    this.GMC.getMyCart().subscribe(data=>{

      data.products.forEach(product=>{
        const result = this.products.find(p=>p.productID === product.productID)
        if(result != undefined){
          result.count++
        } else {
          product.count = 1;
          this.products.push(product);
        }

        this.checkoutTotal += product.productPrice
      });
    
    })
  }
}
