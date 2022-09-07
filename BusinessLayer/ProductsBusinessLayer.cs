using RepoLayer;
using Microsoft.AspNetCore.Http;
using Models;
using Microsoft.AspNetCore.Components;

namespace BusinessLayer;
public class ProductsBusinessLayer
{
    private readonly StoreFrontRepoLayer _repoLayer;
    public ProductsBusinessLayer()
    {

        this._repoLayer = new StoreFrontRepoLayer();
    }


    //Check for existing product before inserting them
    public async Task<Products> InsertProductsAsync(Products product, byte[]? Imagebyte)
    {
        bool m = await this._repoLayer.CheckExisitngProductAsync(product);

        if (m)
        {
            return null;
        }
        else
        {

            Guid id = Guid.NewGuid();

            Products product1 = await this._repoLayer.InsertProductsAsync(product, Imagebyte); ;

            return product1;


        }
       

    }


    //Get product by Product ID
    public async Task<ProductDto?> GetProductByIdAsync(Guid productID)
    {
        ProductDto? p = await this._repoLayer.GetProductByIdAsync(productID);

       
        return p;

    }



    //Get all products

    public async Task<List<ProductDto?>> GetAllProductsAsync()
    {

        List<ProductDto?> productList = await this._repoLayer.GetAllProductsAsync();

       
        return productList;

    }


    

    //Check for existing user before they register
    public async Task<UserProfile> RegisterAsync(UserProfile userprofile, byte[]? UserImagebyte)
    {

        bool u = await this._repoLayer.GetUsersByEmailAsync(userprofile);


        if (u)
        {
            return null;
        }
        else 
        {

            Guid id = Guid.NewGuid();

            UserProfile userprofile1 = await this._repoLayer.RegisterAsync(userprofile, UserImagebyte);

            return userprofile1;


        }




    }

    
 




}
