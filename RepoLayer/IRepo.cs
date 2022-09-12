using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace RepoLayer
{
    public interface IRepo
    {
        Task<User?> GetUserByUsername(string? username);
        Task<UserProfile?> GetProfileByUserID(Guid? userID);
        Task<Stream?> GetProfilePictureByUserID(Guid? userID);
        Task<User?> InsertUserAsync(RegisterDto request);
        Task<UserProfile?> InsertProfileAsync(RegisterDto request);
        Task<Cart?> InsertCartAsync(Guid? userID);
        Task<Cart?> GetCartByUserID(Guid? userID);
    }
}