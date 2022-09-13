using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace BusinessLayer
{
    public interface IBus
    {
        Task<User?> LoginAsync(LoginDto request);
        Task<UserInfoDto?> GetUserInfoAsync(string username);
        Task<Stream?> GetUserPhotoAsync(string username);
        Task<UserInfoDto?> RegisterNewUserAsync(RegisterDto request);
        Task<List<Product?>> GetAllProductsAsync();
        Task<Stream?> GetProductImageAsync(Guid? request);
        Task<Order?> CreateOrderAsync(Guid? userID);
        Task<List<Order?>> GetMyOrdersAsync(Guid? userID);
        Task<SingleOrderDto?> GetOrderAsync(Guid? orderID);
        Task<MyCartDto?> GetMyCartAsync(Guid? userID);
        Task<MyCartDto?> AddProductToCartAsync(Guid? userID, Guid? productID);
    }
}