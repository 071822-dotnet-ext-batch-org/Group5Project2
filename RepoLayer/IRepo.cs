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
        Task<byte[]?> GetProfilePictureByUserID(Guid? userID);
    }
}