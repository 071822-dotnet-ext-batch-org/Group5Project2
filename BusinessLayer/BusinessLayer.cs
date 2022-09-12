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

        Cart? c = await this._repo.GetCartByUserID(u.UserID);

        if (c == null)
        {
            return null;
        }

        bool hasProfilePicture = (p?.ProfilePicture?.Length > 0);

        UserInfoDto uidto = new UserInfoDto(u.UserID, u.Username, p?.ProfileID, p?.ProfileName, p?.ProfileAddress, p?.ProfilePhone, p?.ProfileEmail, c.CartID, hasProfilePicture);
        
        return uidto;
    }

    public async Task<Stream?> GetUserPhotoAsync(string request)
    {
        User? u = await this._repo.GetUserByUsername(request);

        if (u == null)
        {
            return null;
        }

        Stream? photo = await this._repo.GetProfilePictureByUserID(u.UserID);

        if (photo?.Length == 0 || photo == null)
        {
            return null;
        }

        return photo;
    }

    public async Task<UserInfoDto?> RegisterNewUserAsync(RegisterDto request)
    {
        User? u = await this._repo.GetUserByUsername(request.Username);
        UserInfoDto uidto = new UserInfoDto();

        if (u != null)
        {
            uidto.ErrorMessage = "Username already taken";
            return uidto;
        }

        User? nu = await this._repo.InsertUserAsync(request);

        if (nu == null)
        {
            uidto.ErrorMessage = "There was an error creating the user";
            return uidto;
        }

        UserProfile? np = await this._repo.InsertProfileAsync(request);
        
        if (np == null)
        {
            uidto.ErrorMessage = "There was an error creating the profile";
            return uidto;
        }

        Cart? c = await this._repo.InsertCartAsync(nu.UserID);

        if (c == null)
        {
            uidto.ErrorMessage = "There was an error creating the cart";
            return uidto;
        }
        
        uidto.UserID = nu.UserID;
        uidto.Username = nu.Username;
        uidto.ProfileID = np.ProfileID;
        uidto.ProfileName = np.ProfileName;
        uidto.ProfileAddress = np.ProfileAddress;
        uidto.ProfilePhone = np.ProfilePhone;
        uidto.ProfileEmail = np.ProfileEmail;
        uidto.CartID = c.CartID;

        if (np?.ProfilePicture?.Length > 0)
        {
            uidto.HasProfilePicture = true;
        } 
        else 
        {
            uidto.HasProfilePicture = false;
        }

        return uidto;
    }
}
