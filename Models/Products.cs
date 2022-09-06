using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Models;
public class Products
{
 
    public Guid ProductID { get; set; }

    public string? ProductName { get; set; }

    
    [Required(ErrorMessage = "Pick an Image")]
    public IFormFile? ProductImage { get; set; }

    public string? ProductDetails { get; set; }

    public double? ProductPrice { get; set; }

    public DateTime? StockDate { get; set; }

    public int? Stock { get; set; }




}
