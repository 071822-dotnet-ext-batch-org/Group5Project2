import { ProductListService } from './../../Services/product-list-service/product-list.service';
import { ProductListComponent } from './../../components/product-list/product-list.component';


import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Orders } from 'src/app/Models/Orders';


@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {
  
  Orders!: any;
  orderID!: any;
  orderproduct!: any;
  
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

    this.EcommerceAPI.getOrdersById(orderID).subscribe((data: any) => {
      this.orderproduct = data;
      console.log(this.orderproduct);
  })
 }
  
  getallproductOrders(){
  this.orderproduct;
  console.log(this.orderproduct);
  var getOrderedproduct = JSON.stringify(this.orderproduct);
  return getOrderedproduct;
  
 }
 




}
