using Models;
using RepoLayer;

namespace BusinessLayer;


public class Bus : IBus
{
    private readonly IRepo _repo;

    public Bus(IRepo repo)
    {
        _repo = repo;
    }

    public async Task<User?> LoginAsync(LoginDto request)
    {
        User? u = await this._repo.GetUserByUsername(request.Username);

        if (u == null || u.Password != request.Password)
        {
            return null;
        }

        return u;
    }

    public async Task<UserInfoDto?> GetUserInfoAsync(string request)
    {
        User? u = await this._repo.GetUserByUsername(request);

        if (u == null)
        {
            return null;
        }

        UserProfile? p = await this._repo.GetProfileByUserID(u.UserID);

        if (p == null)
        {
            return null;
        }

        UserInfoDto uidto = new UserInfoDto(u.UserID, u.Username, p.ProfileID, p.ProfileName, p.ProfileAddress, p.ProfilePhone, p.ProfileEmail);
        
        return uidto;
    }

    public async Task<byte[]?> GetUserPhotoAsync(string request)
    {
        User? u = await this._repo.GetUserByUsername(request);

        if (u == null)
        {
            return null;
        }

        byte[]? photo = await this._repo.GetProfilePictureByUserID(u.UserID);

        if (photo?.Length == 0 || photo == null)
        {
            return null;
        }

        return photo;
    }
}
