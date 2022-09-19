import { Orders } from "./Orders";
import { Product } from "./Product";

export interface OrderedProducts {
    products: Product[];
    order: Orders;
}