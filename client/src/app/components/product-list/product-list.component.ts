import { Register } from './../Models/Register';
import { Component, OnInit } from '@angular/core';
import { ProductListService } from '../../Services/product-list-service/product-list.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products: any;
  userregister: any;
  data: any;
  

  constructor(private PLS: ProductListService) { }

  ngOnInit(): void {

    this.displayProducts();
  }

  displayProducts(): void {
    this.PLS.getProducts().subscribe(data => {
      this.products = data;
    })
  }

  RegisterUsers(data : Register){
    this.EcommerceAPI.postRegistration(data).subscribe(res => {
      console.log(res);
    })
  }


  

}

