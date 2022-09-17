

import { ProductListService } from './../../Services/product-list-service/product-list.service';
import { ProductListComponent } from './../../components/product-list/product-list.component';


import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Orders } from 'src/app/Models/OrderID';


@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {
  
  getPriorOrders!: string;
  userID!: any;
  orderID!: any;
  orderproduct!: any
  
  constructor(private EcommerceAPI: ProductListService) { }

  ngOnInit(): void {
    
  }

  displayPreviousOrders(userOrderID: string) {
    
    this.EcommerceAPI.getPriorOrdersByUserID(userOrderID).subscribe(data => {
      this.getPriorOrders = data;
       
    
    })

  }
    
  
  getPreviousOrders2(){

    this.getPriorOrders;
    console.log(this?.getPriorOrders);
    var getOrders = JSON.stringify(this.getPriorOrders);
    return getOrders;
    
  }
  
   

  displayOrderedProducts(orderID : string){

    this.EcommerceAPI.getOrdersById(orderID).subscribe(data => {
      this.orderproduct = data;

  })
 }
  
 getallproductOrders(){

  this.orderproduct;
  console.log(this?.orderproduct);
  var getOrderedproduct = JSON.stringify(this.orderproduct);
  return getOrderedproduct;
  
}


  

}
