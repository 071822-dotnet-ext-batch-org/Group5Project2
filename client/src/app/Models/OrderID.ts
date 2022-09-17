export interface OrderID{
    orderID: string;
}

export interface Orders{
    orderID: string;
    orderTotal: number;
    dateOrdered: string;
    dateDelivered: string;
    cancelled: boolean;
    refunded: boolean;
    fK_UserID: string;
   
}