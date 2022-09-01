using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Models
{
    public class ProductDto
    {
        public Guid ProductID { get; set; }

        public string? ProductName { get; set; }

        //public Image? ProductImage { get; set; }
        //public byte[]? ProductImage { get; set; }

        public string? ProductImage { get; set; }

        public string? ProductDetails { get; set; }

        public double? ProductPrice { get; set; }

        public DateTime? StockDate { get; set; }

        public int? Stock { get; set; }
    }
}
