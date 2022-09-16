import { Component, OnInit } from '@angular/core';
import { ProductListService } from '../../Services/product-list-service/product-list.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products: any;

  constructor(private PLS: ProductListService) { }

  ngOnInit(): void {
  }

  displayProducts(): void {
    this.PLS.getProducts().subscribe(data => {
      this.products = data;
    })
  }

}
