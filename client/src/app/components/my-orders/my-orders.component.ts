import { ProductListService } from './../../Services/product-list-service/product-list.service';
import { ProductListComponent } from './../../components/product-list/product-list.component';


import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Product } from 'src/app/Models/Product';
import { OrderedProducts } from 'src/app/Models/OrderedProducts';


@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {
  
  Orders!: any;
  orderID!: any;
  orderproduct!: OrderedProducts;
  products: Product[] = [];
  loading: boolean = false;
  errorMessage: string = "";
  
  constructor(private EcommerceAPI: ProductListService) { }

  ngOnInit(): void {
    
    this.displayOrders();
    
  }
  
  //Display all orders
  displayOrders() : void {
    
    this.EcommerceAPI.getOrders().subscribe((data: any) => {
      this.Orders= data;
       
    })

  }
    
   //Display all ordered products by ID
  displayOrderedProducts(orderID : any) : void {
    this.products = [];
    this.loading = true;

    this.EcommerceAPI.getOrdersById(orderID).subscribe({
      next: data => {
        this.orderproduct = data;
        data.products.forEach(product=>{
          const result = this.products.find(p=>p.productID === product.productID)
          if(result != undefined){
            result.count++
          } else {
            product.count = 1;
            this.products.push(product);
          }
        });
        console.log(this.products); 
        this.loading = false;
      },
      error: err => {
        this.loading = false;
        this.errorMessage = "There was an error retrieving the product data";
      }
    })
  }
  
}
