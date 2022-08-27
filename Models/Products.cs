using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;
public class Products
{
 
    public Guid ProductID { get; set; }

    public string? ProductName { get; set; }

    public IFormFile? ProductImage { get; set; }

    public string? ProductDetails { get; set; }

    public double? ProductPrice { get; set; }

    public DateTime? StockDate { get; set; }

    public int? Stock { get; set; }




}
