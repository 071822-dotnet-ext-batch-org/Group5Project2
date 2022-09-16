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

    public async Task<User?> LoginAsync(string? userID)
    {
        User? u = await this._repo.GetUserByUserIDAsync(userID);

        return u;
    }

    public async Task<UserInfoDto?> GetUserInfoAsync(string? userID)
    {
        User? u = await this._repo.GetUserByUserIDAsync(userID);

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

        UserInfoDto uidto = new UserInfoDto(p?.ProfileName, p?.ProfileAddress, p?.ProfilePhone, p?.ProfileEmail, c.CartID, p?.ProfilePicture);
        
        return uidto;
    }

    public async Task<UserInfoDto?> RegisterNewUserAsync(string? userID, string? name, string? email, string? picture)
    {
        User? u = await this._repo.GetUserByUserIDAsync(userID);
        UserInfoDto uidto = new UserInfoDto();

        if (u != null)
        {
            uidto.ErrorMessage = "Username already exists";
            return uidto;
        }

        User? nu = await this._repo.InsertUserAsync(userID);

        if (nu == null)
        {
            uidto.ErrorMessage = "There was an error creating the user";
            return uidto;
        }

        UserProfile? np = await this._repo.InsertProfileAsync(name, email, picture, userID);
        
        if (np == null)
        {
            uidto.ErrorMessage = "There was an error creating the profile";
            return uidto;
        }

        Cart? c = await this._repo.InsertCartAsync(userID);

        if (c == null)
        {
            uidto.ErrorMessage = "There was an error creating the cart";
            return uidto;
        }
        
        uidto.ProfileName = np.ProfileName;
        uidto.ProfilePhone = np.ProfilePhone;
        uidto.ProfileEmail = np.ProfileEmail;
        uidto.ProfilePicture = np.ProfilePicture;
        uidto.CartID = c.CartID;

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

    public async Task<Order?> CreateOrderAsync(string? userID)
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

    public async Task<List<Order?>> GetMyOrdersAsync(string? userID)
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

    public async Task<MyCartDto?> GetMyCartAsync(string? userID)
    {
        Cart? c = await this._repo.GetCartByUserIDAsync(userID);
        
        if (c == null)
        {
            return null;
        }

        List<Product?> productList = await this._repo.GetProductsFromCartAsync(c.CartID);

        return new MyCartDto(productList, c);
    }

    public async Task<MyCartDto?> AddProductToCartAsync(string? userID, Guid? productID)
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
        Order? e = await this._repo.CreateNewOrderAsync(rr,id);
       
        return e;
}
     public async Task<User?> UpdateAccountDetailsAsync(User user )
    {
        Guid id = Guid.NewGuid();
        User? aa = await this._repo.UpdateAccountDetailsAsync(user, id);
        return aa;

    }
    public async Task<bool> UpdateProductImage(Stream file, Guid productId)
    {
        Products? updatedTicket = await this._repo.UpdateProductImage(productId);

        if (UpdateProductImage == null) return false;

        using BinaryReader reader = new BinaryReader(file);

        byte[] photo = reader.ReadBytes((int)file.Length);

        bool isSuccess = await this._repo.UpdateProductImage(photo, productId);

        return isSuccess;
    }
}
