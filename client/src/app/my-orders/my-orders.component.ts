import { ProductListService } from './../Services/product-list-service/product-list.service';
import { ProductListComponent } from './../components/product-list/product-list.component';

import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {
  
  getPriorOrders!: string;
  userID!: any;
  
  constructor(private EcommerceAPI: ProductListService) { }

  ngOnInit(): void {
    
  }

  displayPreviousOrders(userOrderID: string) {
    
    this.EcommerceAPI.getPriorOrdersByUserID(userOrderID).subscribe(data => {
      this.getPriorOrders = data;
       
      // console.log (getOrders );
      
      
    })

  }
  
  getPreviousOrders2(){

    this.getPriorOrders;
    var getOrders = JSON.stringify(this.getPriorOrders);
    return getOrders;
  }
  

}
