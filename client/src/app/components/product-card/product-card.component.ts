import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Product } from 'src/app/Models/Product';
import { FormControl } from '@angular/forms';
import { AddToCartService } from 'src/app/Services/add-to-cart/add-to-cart.service';
import { GetMyCartService } from 'src/app/Services/get-my-cart/get-my-cart.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {

  total: number = 0;
  count = new FormControl('');
  @Input() product: Product | undefined;
  @Output("displayCart") displayCart: EventEmitter<any> = new EventEmitter();

  constructor(
    private ATC: AddToCartService,
    private GMC: GetMyCartService
  ) { }

  ngOnInit(): void {
    if (this.product != undefined) {
      this.total = this.product.productPrice * this.product.count;
      this.count.setValue(this.product.count.toString());
    }
  }

  changeCount(): void {
    if (this.count.value != null && this.product != undefined) {

      const countValue = parseInt(this.count.value);

      if (countValue > this.product.count) {
        const amountToAdd = countValue - this.product.count;
        this.ATC.addCountToCart(this.product.productID, amountToAdd).subscribe({
          next: cart => {
            this.displayCart.emit();
          },
        })
      }

      if (countValue < this.product.count) {
        const amountToDelete = this.product.count - countValue;
        this.ATC.deleteCountFromCart(this.product.productID, amountToDelete).subscribe({
          next: cart => {
            this.displayCart.emit();
          },
        })
      }

    }
  }

}
