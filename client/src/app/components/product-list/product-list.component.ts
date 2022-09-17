import { Component, OnInit } from '@angular/core';
import { AddToCartService } from 'src/app/Services/add-to-cart/add-to-cart.service';
import { ProductListService } from '../../Services/product-list-service/product-list.service';
import { AuthService } from '@auth0/auth0-angular';
import { DataSharingService } from 'src/app/Services/data-sharing/data-sharing.service';
import { GetUserInfoService } from 'src/app/Services/get-user-info/get-user-info.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products: any;
  userregister: any;
 
  

  constructor(
    private PLS: ProductListService,
    private ATC: AddToCartService,
    private DSS: DataSharingService,
    private UIS: GetUserInfoService,
    public auth: AuthService  
  ) { }

  ngOnInit(): void {

    this.displayProducts();
  }

  displayProducts(): void {
    this.PLS.getProducts().subscribe((data: any) => {
      this.products = data;
    })
  }

  addToCart(productID: string): void {
    this.ATC.addToCart(productID).subscribe(data => {
      if(data){
        console.log('added to cart')
        
        this.UIS.getUserInfo().subscribe(data=>{
          console.log(data.cart.cartItems);
          this.DSS.updateCart(data.cart.cartItems);
        });

      } else {
        console.log('something went wrong')
      }
    });


  }

}

