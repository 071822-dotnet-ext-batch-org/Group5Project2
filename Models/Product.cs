using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        public Guid? ProductID {get; set;}
        public string? ProductName {get; set;}
        public decimal ProductPrice {get; set;}
        public string? ProductDetails {get; set;}
        public DateTime? StockDate {get; set;}
        public int? Stock {get; set;}
        public DateTime? DateCreated {get; set;}
        public DateTime? DateModified {get; set;}

        public Product()
        {
        }

        public Product(Guid? productID, string? productName, decimal productPrice, string? productDetails, DateTime? stockDate, int? stock, DateTime? dateCreated, DateTime? dateModified)
        {
            ProductID = productID;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductDetails = productDetails;
            StockDate = stockDate;
            Stock = stock;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
    }
}