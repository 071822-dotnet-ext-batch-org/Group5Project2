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
     
        public Guid? CartID;
        public decimal? CartTotal;
        public int? CartItems;
        public Guid? Fk_UserID;
    }
}