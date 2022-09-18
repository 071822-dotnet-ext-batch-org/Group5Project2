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

    this.EcommerceAPI.getOrdersById(orderID).subscribe(data => {
      this.orderproduct = data;
      this.products = data.products
      console.log(this.orderproduct);
  })
 }
  
}
