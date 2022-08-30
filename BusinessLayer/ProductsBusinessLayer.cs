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




}
