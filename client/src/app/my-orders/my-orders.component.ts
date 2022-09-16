
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { EcommerceAPIService } from '../Services/ecommerce-api.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {
  
  getPriorOrders!: string;
  userID!: any;
  
  constructor(private EcommerceAPI: EcommerceAPIService) { }

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
