using System.Security.Claims;
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

    public async Task<UserInfoDto?> GetUserInfoAsync(string? userID, IEnumerable<Claim> claims)
    {
        UserProfile? p = await this._repo.GetProfileByUserIDAsync(userID);
        string? name = claims.FirstOrDefault(c=>c.Type.Equals("myinfo/name"))?.Value;
        string? email = claims.FirstOrDefault(c=>c.Type.Equals("myinfo/email"))?.Value;
        string? picture = claims.FirstOrDefault(c=>c.Type.Equals("myinfo/picture"))?.Value;

        if (p == null)
        {
            p = await this._repo.InsertProfileAsync(name, email, picture, userID);
        }

        Cart? c = await this._repo.GetCartByUserIDAsync(userID);

        if (c == null)
        {
            c = await this._repo.InsertCartAsync(userID);
        }

        UserInfoDto uidto = new UserInfoDto(p?.ProfileName, p?.ProfileAddress, p?.ProfilePhone, p?.ProfileEmail, c, p?.ProfilePicture);
        
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

    public async Task<bool> AddProductToCartAsync(string? userID, Guid? productID)
    {
        Cart? c = await this._repo.GetCartByUserIDAsync(userID);

        if (c == null)
        {
            return false;
        }

        Product? p = await this._repo.GetProductByProductIDAsync(productID);

        if (p == null)
        {
            return false;
        }

        bool addedToCartSuccess = await this._repo.AddProductToCartAsync(c.CartID, productID);

        return addedToCartSuccess;
    }

     public async Task<Order?> CreateNewOrderAsync(UpdateNewOrderDto rr)
    {
         Guid id = Guid.NewGuid();
        Order? e = await this._repo.CreateNewOrderAsync(rr,id);
       
        return e;
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
