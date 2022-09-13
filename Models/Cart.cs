using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Cart
    {
        public Cart()
        {
        }

        public Cart(Guid? cartID, decimal? cartTotal, int? cartItems, Guid? fk_UserID)
        {
            CartID = cartID;
            CartTotal = cartTotal;
            CartItems = cartItems;
            Fk_UserID = fk_UserID;
        }
     
        public Guid? CartID {get; set;}
        public decimal? CartTotal {get; set;}
        public int? CartItems {get; set;}
        public Guid? Fk_UserID {get; set;}
    }
}