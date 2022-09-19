import { Cart } from "./Cart";

export interface UserInfo {
    profileID: string;
    profileName: string;
    profileAddress: string;
    profilePhone: string;
    profileEmail: string;
    profilePicture: string;
    cart: Cart;
    errorMessage: string;
}