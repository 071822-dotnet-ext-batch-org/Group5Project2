using RepoLayer;
using Microsoft.AspNetCore.Http;
using Models;



namespace BusinessLayer;
public class ProductsBusinessLayer
{
    private readonly StoreFrontRepoLayer _repoLayer;
    public ProductsBusinessLayer()
    {

        this._repoLayer = new StoreFrontRepoLayer();
    }

    public async Task InsertProductsAsync(Products product, byte[]? Imagebyte)
    {

        await this._repoLayer.InsertProductsAsync(product, Imagebyte);

    }


    public async Task<ProductDto?> GetProductByIdAsync(Guid productID)
    {
        ProductDto? p = await this._repoLayer.GetProductByIdAsync(productID);

       
        return p;

    }

    //TODO
    public async Task<ProductDto?>GetImageAsync(Guid productID, dynamic Imageproduct)
    {

        ProductDto? getImage = await this._repoLayer.GetImageAsync(productID, Imageproduct);

        ProductDto? m = await this._repoLayer.GetImageAsync(productID, Imageproduct);

        return m;
    }




}
