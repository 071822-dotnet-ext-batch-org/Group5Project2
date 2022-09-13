using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class MyCartDto
    {
        public MyCartDto()
        {
        }

        public MyCartDto(List<Product?> products, Cart? cart)
        {
            Products = products;
            Cart = cart;
        }

        public List<Product?> Products {get; set;} = new List<Product?>();
        public Cart? Cart {get; set;}
    }
}