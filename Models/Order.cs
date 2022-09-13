using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public Order()
        {
        }

        public Order(Guid? orderID, decimal? orderTotal, DateTime? dateOrdered, DateTime? dateDelivered, bool? cancelled, bool? refunded, Guid? fK_UserID)
        {
            OrderID = orderID;
            OrderTotal = orderTotal;
            DateOrdered = dateOrdered;
            DateDelivered = dateDelivered;
            Cancelled = cancelled;
            Refunded = refunded;
            FK_UserID = fK_UserID;
        }

        public Guid? OrderID {get; set;}
        public decimal? OrderTotal {get; set;}
        public DateTime? DateOrdered {get; set;}
        public DateTime? DateDelivered {get; set;}
        public bool? Cancelled {get; set;}
        public bool? Refunded {get; set;}
        public Guid? FK_UserID {get; set;}
    }
}