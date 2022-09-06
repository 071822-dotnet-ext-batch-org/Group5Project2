using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Configuration;


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


        Products product1 = await this._PostProduct.InsertProductsAsync(product, Imagebyte);

        if (product1 != null)
        {


            return Ok(new { status = true, message = "Product Posted Successfully" });

        }
        else
        {

            return BadRequest("This product already exists in the DataBase.");
        }

        
    }//EoM




    //This API will get products by ID
    [HttpGet("GetProductByIdAsync")]
    public async Task<ActionResult<ProductDto?>> GetProductByIdAsync(Guid productID)
    {
        
        ProductDto? p = await this._PostProduct.GetProductByIdAsync(productID);

        //return File(p.ProductImage, "image/png"); 


        return Ok(p);

    }//EOM





    //Register new user profile
    [HttpPost("RegisterAsync")]
    public async Task<ActionResult> RegisterAsync([FromForm] UserProfile userprofile)
    {

        // This convert image to byte array
        var image = userprofile.ProfilePicture;

        byte[]? UserImagebyte = Array.Empty<byte>();

        if (image != null)
        {
            await using var memoryStream = new MemoryStream();
            await image!.CopyToAsync(memoryStream);
            UserImagebyte = memoryStream.ToArray();

        }


        UserProfile userprofile1 = await this._PostProduct.RegisterAsync(userprofile, UserImagebyte);
        if (userprofile1 != null)
        {


            return Ok(new { status = true, message = "Profile Successfully Created" });

        }
        else 
        { 

           return BadRequest("This user already exists in the DataBase.");
        }




}//EoM







}
