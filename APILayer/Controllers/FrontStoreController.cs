using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.IO.Pipes;

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


    //This API insert products 
    [HttpPost("InsertProductsAsync")]

    public async Task<ActionResult> InsertProductsAsync( [FromForm] Products product)
    {

          
         // Get Image
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

    }//EoC














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
