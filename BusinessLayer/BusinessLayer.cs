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
        User? u = await this._repo.GetUserByUsernameAsync(request.Username);

        if (u == null || u.Password != request.Password)
        {
            return null;
        }

        return u;
    }

    public async Task<UserInfoDto?> GetUserInfoAsync(string request)
    {
        User? u = await this._repo.GetUserByUsernameAsync(request);

        if (u == null)
        {
            return null;
        }

        UserProfile? p = await this._repo.GetProfileByUserIDAsync(u.UserID);

        if (p == null)
        {
            return null;
        }

        Cart? c = await this._repo.GetCartByUserIDAsync(u.UserID);

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
        User? u = await this._repo.GetUserByUsernameAsync(request);

        if (u == null)
        {
            return null;
        }

        Stream? photo = await this._repo.GetProfilePictureByUserIDAsync(u.UserID);

        if (photo?.Length == 0 || photo == null)
        {
            return null;
        }

        return photo;
    }

    public async Task<UserInfoDto?> RegisterNewUserAsync(RegisterDto request)
    {
        User? u = await this._repo.GetUserByUsernameAsync(request.Username);
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

    public async Task<List<Product?>> GetAllProductsAsync()
    {
        return await this._repo.GetAllProductsAsync();
    }

    public async Task<Stream?> GetProductImageAsync(Guid? request)
    {
        Product? p = await this._repo.GetProductByProductIDAsync(request);

        if (p == null)
        {
            return null;
        }

        Stream? image = await this._repo.GetProductImageByProductIDAsync(p.ProductID);

        if (image?.Length == 0 || image == null)
        {
            return null;
        }

        return image;
    }

    public async Task<Order?> CreateOrderAsync(Guid? userID)
    {
        Cart? c = await this._repo.GetCartByUserIDAsync(userID);

        if (c == null)
        {
            return null;
        }

        List<Product?> productList = await this._repo.GetProductsFromCartAsync(c.CartID);

        if (productList.Count == 0)
        {
            return null;
        }

        decimal? orderTotal = productList.Sum(product => product?.ProductPrice);

        Guid orderID = Guid.NewGuid();

        Order? o = await this._repo.InsertOrderAsync(orderID, userID, orderTotal);

        List<Guid?> productIDs = productList.Select(product => product?.ProductID).ToList<Guid?>();

        bool insertOrdersProductsSuccess = await this._repo.InsertOrdersProductsAsync(productIDs, o?.OrderID);

        if (!insertOrdersProductsSuccess)
        {
            return null;
        }

        bool emptyCartSuccess = await this._repo.DeleteAllItemsFromCartByCartIDAsync(c.CartID);
        
        if(!emptyCartSuccess)
        {
            return null;
        }

        return o;
    }

    public async Task<List<Order?>> GetMyOrdersAsync(Guid? userID)
    {
        return await this._repo.GetMyOrdersAsync(userID);
    }

    public async Task<SingleOrderDto?> GetOrderAsync(Guid? orderID)
    {
        Order? o = await this._repo.GetOrderByOrderIDAsync(orderID);

        if (o == null)
        {
            return null;
        }

        List<Product?> productList = await this._repo.GetProductsInOrderAsync(orderID);

        return new SingleOrderDto(productList, o);
    }

    public async Task<MyCartDto?> GetMyCartAsync(Guid? userID)
    {
        Cart? c = await this._repo.GetCartByUserIDAsync(userID);
        
        if (c == null)
        {
            return null;
        }

        List<Product?> productList = await this._repo.GetProductsFromCartAsync(c.CartID);

        return new MyCartDto(productList, c);
    }

    public async Task<MyCartDto?> AddProductToCartAsync(Guid? userID, Guid? productID)
    {
        Cart? c = await this._repo.GetCartByUserIDAsync(userID);

        if (c == null)
        {
            return null;
        }

        Product? p = await this._repo.GetProductByProductIDAsync(productID);

        if (p == null)
        {
            return null;
        }

        bool addedToCartSuccess = await this._repo.AddProductToCartAsync(c.CartID, productID);

        return await GetMyCartAsync(userID);
    }

     public async Task<Order?> CreateNewOrderAsync(UpdateNewOrderDto rr)
    {
         Guid id = Guid.NewGuid();
        Order? e = await this._repoLayer.CreateNewOrderAsync(rr,id);
       
        return e;
}
     public async Task<Users?> UpdateAccountDetailsAsync(Users user )
    {
        Guid id = Guid.NewGuid();
        Users? aa = await this._repoLayer.UpdateAccountDetailsAsync(user, id);
        return aa;

    }
    public async Task<bool> UpdateProductImage(Stream file, Guid productId)
    {
        Products? updatedTicket = await this._repoLayer.UpdateProductImage(productId);

        if (UpdateProductImage == null) return false;

        using BinaryReader reader = new BinaryReader(file);

        byte[] photo = reader.ReadBytes((int)file.Length);

        bool isSuccess = await this._repoLayer.UpdateProductImage(photo, productId);

        return isSuccess;
    }
}
