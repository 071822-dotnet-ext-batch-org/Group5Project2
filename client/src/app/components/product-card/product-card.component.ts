import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/Models/Product';
import { DataSharingService } from 'src/app/Services/data-sharing/data-sharing.service';
import { GetMyCartService } from 'src/app/Services/get-my-cart/get-my-cart.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {

  products: Product[] = [];
  checkoutTotal: number = 0;

  constructor(
    private GMC: GetMyCartService,
    private DSS: DataSharingService
  ) { 
    this.DSS.getUpdatedCheckoutProducts().subscribe(updateProducts => this.products = updateProducts);
  }

  ngOnInit(): void {
    this.displayCart();
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

      });
      
      this.checkoutTotal = data.cart.cartTotal
      this.DSS.updateCheckoutTotal(this.checkoutTotal);
      
    })
  }

}
