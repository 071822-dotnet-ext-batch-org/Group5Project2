import { Register } from './../Models/Register';
import { Component, OnInit } from '@angular/core';
import { EcommerceAPIService } from '../Services/ecommerce-api.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products: any;
  userregister: any;
  data: any;
  

  constructor(private EcommerceAPI: EcommerceAPIService) { }

  ngOnInit(): void {

    this.displayProducts();
  }

  displayProducts(): void {
    this.EcommerceAPI.getProducts().subscribe(data => {
      this.products = data;
    })
  }

  RegisterUsers(data : Register){
    this.EcommerceAPI.postRegistration(data).subscribe(res => {
      console.log(res);
    })
  }


  

}

