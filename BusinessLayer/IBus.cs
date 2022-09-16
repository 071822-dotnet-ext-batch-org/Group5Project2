using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace BusinessLayer
{
    public interface IBus
    {
        Task<User?> LoginAsync(string? userID);
        Task<UserInfoDto?> GetUserInfoAsync(string? userID);
        Task<UserInfoDto?> RegisterNewUserAsync(string? auth0id, string? name, string? email, string? picture);
        Task<List<Product?>> GetAllProductsAsync();
        Task<Stream?> GetProductImageAsync(Guid? request);
        Task<Order?> CreateOrderAsync(string? userID);
        Task<List<Order?>> GetMyOrdersAsync(string? userID);
        Task<SingleOrderDto?> GetOrderAsync(Guid? orderID);
        Task<MyCartDto?> GetMyCartAsync(string? userID);
        Task<MyCartDto?> AddProductToCartAsync(string? userID, Guid? productID);
    }
}