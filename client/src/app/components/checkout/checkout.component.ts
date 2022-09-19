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
  loading: boolean = false;
  loadingProducts: boolean = false;

  constructor(
    private CHK: CheckoutService,
    private GMC: GetMyCartService,
    private DSS: DataSharingService
  ) { }

  ngOnInit(): void {
    this.loadingProducts = true;
    this.displayCart();
  }

  checkout(): void {
    this.loading = true;

    this.CHK.checkout().subscribe({
      next: data => {
        this.orderMessage=data.message;
        this.checkoutTotal = 0;
        this.products = [];
        this.loading = false;
        this.DSS.updateCart(0);
      },
      error: err => {
        console.log(err)
        this.orderMessage=err.error.message
        this.loading = false
      }
    })
  }

  displayCart(): void {
    this.GMC.getMyCart().subscribe(data=>{
      this.products = [];
      data.products.forEach(product=>{
        const result = this.products.find(p=>p.productID === product.productID)
        if(result != undefined){
          result.count++
        } else {
          product.count = 1;
          this.products.push(product);
        }

      });
      
      this.DSS.updateCart(data.cart.cartItems);
      this.checkoutTotal = data.cart.cartTotal;
      this.loadingProducts = false;
      
    })
  }
}
