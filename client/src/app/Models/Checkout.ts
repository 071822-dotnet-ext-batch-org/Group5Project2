import { Cart } from "./Cart";
import { Product } from "./Product";

export interface Checkout {
    products: Product[];
    cart: Cart;
}