using System;
namespace Models
{
    public class UpdateNewOrderDto
    {
        public UpdateNewOrderDto(Guid? orderId, decimal? orderTotal, DateTime? dateOrder, DateTime? dateDelivered, Boolean? cancelled, Boolean refunded)
        {
            OrderId = orderId;
            OrderTotal = orderTotal;
            DateOrder = dateOrder;
            this.dateDelivered = dateDelivered;
            Cancelled = cancelled;
            Refunded = refunded;
        }

        public Guid? OrderId { get; set; }
        public decimal? OrderTotal { get; set; }
        public DateTime? DateOrder { get; set; }
        public DateTime? dateDelivered { get; set; }
        public Boolean? Cancelled { get; set; }
        public Boolean? Refunded { get; set; }
    }
}

