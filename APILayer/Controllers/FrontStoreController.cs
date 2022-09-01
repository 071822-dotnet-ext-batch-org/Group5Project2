using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.IO.Pipes;
using System.Drawing;

namespace APILayer.Controllers;

[ApiController]
[Route("[controller]")]
public class FrontStoreController : ControllerBase
{

    
    private readonly ProductsBusinessLayer _PostProduct;

    public FrontStoreController()
    {

        this._PostProduct = new ProductsBusinessLayer();

    }


    //This API will insert products into inventory
    [HttpPost("InsertProductsAsync")]

    public async Task<ActionResult> InsertProductsAsync( [FromForm] Products product)
    {

          
         // This convert image to byte array
        var image = product.ProductImage;

        byte[]? Imagebyte = Array.Empty<byte>();

        if (image != null)
        {
            await using var memoryStream = new MemoryStream();
            await image!.CopyToAsync(memoryStream);
            Imagebyte = memoryStream.ToArray();

        }

        
        await this._PostProduct.InsertProductsAsync(product, Imagebyte);


        return Ok(new { status = true, message = "Product Posted Successfully" });

    }//EoM


    //This API will get products by ID
    [HttpGet("GetProductByIdAsync")]
    public async Task<ActionResult<ProductDto?>> GetProductByIdAsync(Guid productID)
    {
        
        ProductDto? p = await this._PostProduct.GetProductByIdAsync(productID);

        //return File(p.ProductImage, "image/png"); 


        return Ok(p);

    }//EOM











    // private static readonly string[] Summaries = new[]
    // {
    //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    // };

    // private readonly ILogger<WeatherForecastController> _logger;

    // public WeatherForecastController(ILogger<WeatherForecastController> logger)
    // {
    //     _logger = logger;
    // }

    // [HttpGet(Name = "GetWeatherForecast")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //     {
    //         Date = DateTime.Now.AddDays(index),
    //         TemperatureC = Random.Shared.Next(-20, 55),
    //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //     })
    //     .ToArray();
    // }




}
