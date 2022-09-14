using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace RepoLayer
{
    public interface IRepo
    {
        Task<User?> GetUserByUsernameAsync(string? username);
        Task<UserProfile?> GetProfileByUserIDAsync(Guid? userID);
        Task<Stream?> GetProfilePictureByUserIDAsync(Guid? userID);
        Task<User?> InsertUserAsync(RegisterDto request);
        Task<UserProfile?> InsertProfileAsync(RegisterDto request);
        Task<Cart?> InsertCartAsync(Guid? userID);
        Task<Cart?> GetCartByUserIDAsync(Guid? userID);
        Task<List<Product?>> GetAllProductsAsync();
        Task<Product?> GetProductByProductIDAsync(Guid? request);
        Task<Stream?> GetProductImageByProductIDAsync(Guid? request);
        Task<List<Product?>> GetProductsFromCartAsync(Guid? cartID);
        Task<Order?> InsertOrderAsync(Guid orderID, Guid? userID, decimal? orderTotal);
        Task<Order?> GetOrderByOrderIDAsync(Guid? orderID);
        Task<bool> InsertOrdersProductsAsync(List<Guid?> productIDs, Guid? orderID);
        Task<bool> DeleteAllItemsFromCartByCartIDAsync(Guid? cartID);
        Task<List<Order?>> GetMyOrdersAsync(Guid? userID);
        Task<List<Product?>> GetProductsInOrderAsync(Guid? orderID);
        Task<bool> AddProductToCartAsync(Guid? cartID, Guid? productID);
        Task<Order?> CreateNewOrderAsync(UpdateNewOrderDto rr, Guid id);
        Task<User?> UpdateAccountDetailsAsync(User user, Guid id);
        Task<Products?> UpdateProductImage(Guid productId);
        Task<bool> UpdateProductImage(byte[] photo, Guid productId);
    }
}