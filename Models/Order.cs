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

        public Order(Guid? orderID, decimal? orderTotal, DateTime? dateOrdered, DateTime? dateDelivered, bool? cancelled, bool? refunded, string? fk_userID)
        {
            OrderID = orderID;
            OrderTotal = orderTotal;
            DateOrdered = dateOrdered;
            DateDelivered = dateDelivered;
            Cancelled = cancelled;
            Refunded = refunded;
            FK_UserID = fk_userID;
        }

        public Guid? OrderID {get; set;}
        public decimal? OrderTotal {get; set;}
        public DateTime? DateOrdered {get; set;}
        public DateTime? DateDelivered {get; set;}
        public bool? Cancelled {get; set;}
        public bool? Refunded {get; set;}
        public string? FK_UserID {get; set;}
    }
}