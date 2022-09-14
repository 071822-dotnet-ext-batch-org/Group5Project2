import { Component, OnInit } from '@angular/core';
import { EcommerceAPIService } from '../Services/ecommerce-api.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products: any;

  constructor(private EcommerceAPI: EcommerceAPIService) { }

  ngOnInit(): void {
  }

  displayProducts(): void {
    this.EcommerceAPI.getProducts().subscribe(data => {
      this.products = data;
    })
  }

}
