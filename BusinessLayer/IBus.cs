using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Models;

namespace BusinessLayer
{
    public interface IBus
    {
        Task<UserInfoDto?> GetUserInfoAsync(string? userID, IEnumerable<Claim> claims);
        Task<List<Product?>> GetAllProductsAsync();
        Task<Stream?> GetProductImageAsync(Guid? request);
        Task<Order?> CreateOrderAsync(string? userID);
        Task<List<Order?>> GetMyOrdersAsync(string? userID);
        Task<SingleOrderDto?> GetOrderAsync(Guid? orderID);
        Task<MyCartDto?> GetMyCartAsync(string? userID);
        Task<bool> AddProductToCartAsync(string? userID, Guid? productID);
    }
}