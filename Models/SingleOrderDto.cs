using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class SingleOrderDto
    {
        public List<Product?> Products {get; set;} = new List<Product?>();
        public Order? Order {get; set;}

        public SingleOrderDto()
        {
        }

        public SingleOrderDto(List<Product?> products, Order? order)
        {
            Products = products;
            Order = order;
        }
    }
}