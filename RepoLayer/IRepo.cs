using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace RepoLayer
{
    public interface IRepo
    {
        Task<UserProfile?> GetProfileByUserIDAsync(string? userID);
        Task<UserProfile?> InsertProfileAsync(string? name, string? email, string? picture, string? userID);
        Task<Cart?> InsertCartAsync(string? userID);
        Task<Cart?> GetCartByUserIDAsync(string? userID);
        Task<List<Product?>> GetAllProductsAsync();
        Task<Product?> GetProductByProductIDAsync(Guid? request);
        Task<Stream?> GetProductImageByProductIDAsync(Guid? request);
        Task<List<Product?>> GetProductsFromCartAsync(Guid? cartID);
        Task<Order?> InsertOrderAsync(Guid orderID, string? userID, decimal? orderTotal);
        Task<Order?> GetOrderByOrderIDAsync(Guid? orderID);
        Task<bool> InsertOrdersProductsAsync(List<Guid?> productIDs, Guid? orderID);
        Task<bool> DeleteAllItemsFromCartByCartIDAsync(Guid? cartID);
        Task<List<Order?>> GetMyOrdersAsync(string? userID);
        Task<List<Product?>> GetProductsInOrderAsync(Guid? orderID);
        Task<bool> AddProductToCartAsync(Guid? cartID, Guid? productID);
        Task<Order?> CreateNewOrderAsync(UpdateNewOrderDto rr, Guid id);
        Task<Products?> UpdateProductImage(Guid productId);
        Task<bool> UpdateProductImage(byte[] photo, Guid productId);
        Task<bool> UpdateUserAddressAsync(string? userID, string address);
        Task<bool> UpdateUserPhoneAsync(string? userID, string phone);
    }
}