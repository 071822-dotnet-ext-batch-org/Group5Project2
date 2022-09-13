using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;
using System.Data;

namespace RepoLayer;

public class Repo : IRepo
{
    private readonly IConfiguration _config;
    private readonly SqlConnection _conn;

    public Repo(IConfiguration config)
    {
        _config = config;
        _conn = new SqlConnection(_config["ConnectionStrings:project2ApiDB"]);
    }

    public async Task<User?> GetUserByUsernameAsync(string? username)
    {
        string sql = $"SELECT * FROM Users WHERE username = @username";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@username", username);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                User u = new User(ret.GetGuid(0), ret.GetString(1), ret.GetString(2), ret.GetDateTime(3), ret.GetDateTime(4));
                _conn.Close();
                return u;
            }

            _conn.Close();
            return null;
        }
    }

    public async Task<UserProfile?> GetProfileByUserIDAsync(Guid? userID)
    {
        
        string sql = $"SELECT * FROM Profiles WHERE fk_userID = @userID";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@userID", userID);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                UserProfile p = new UserProfile(ret.GetGuid(0), ret.GetString(1), ret.GetString(2), ret.GetString(3), ret.GetString(4), ret.GetStream(5), ret.GetGuid(6), ret.GetDateTime(7), ret.GetDateTime(8));
                _conn.Close();
                return p;
            }

            _conn.Close();
            return null;
        }
    }

    public async Task<Stream?> GetProfilePictureByUserIDAsync(Guid? userID)
    {
        string sql = $"SELECT profilePicture FROM Profiles WHERE fk_userID = @userID";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@userID", userID);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                Stream photo = ret.GetStream(0);
                _conn.Close();
                return photo;
            }


            _conn.Close();
            return null;
        }
    }

    public async Task<User?> InsertUserAsync(RegisterDto request)
    {
        string sql = $"INSERT INTO Users (username, password) VALUES (@username, @password)";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@username", request.Username);
            command.Parameters.AddWithValue("@password", request.Password);
            
            _conn.Open();
            bool ret = (await command.ExecuteNonQueryAsync()) == 1;
            _conn.Close();

            if (!ret)
            {   
                return null;
            }

            return await GetUserByUsernameAsync(request.Username);
        }
    }


    public async Task<UserProfile?> InsertProfileAsync(RegisterDto request)
    {
        string sql = $"INSERT INTO Profiles (profileName, profileAddress, profilePhone, profileEmail, profilePicture, fk_userID) VALUES (@profileName, @profileAddress, @profilePhone, @profileEmail, @profilePicture, @fk_userID)";
        Guid? userID = (await GetUserByUsernameAsync(request.Username))?.UserID;
        Stream? profilePictureStream = request.ProfilePicture?.OpenReadStream();
        
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@profileName", request.ProfileName);
            command.Parameters.AddWithValue("@profileAddress", request.ProfileAddress);
            command.Parameters.AddWithValue("@profilePhone", request.ProfilePhone);
            command.Parameters.AddWithValue("@profileEmail", request.ProfileEmail);
            command.Parameters.AddWithValue("@profilePicture", profilePictureStream);
            command.Parameters.AddWithValue("@fk_userID", userID);

            _conn.Open();
            bool ret = (await command.ExecuteNonQueryAsync()) == 1;
            _conn.Close();

            if (!ret)
            {   
                return null;
            }

            return await GetProfileByUserIDAsync(userID);
        }
    }

    public async Task<Cart?> InsertCartAsync(Guid? userID)
    {
        string sql = $"INSERT INTO Carts (cartTotal, cartItems, fk_userID) VALUES (0, 0, @fk_userID)";
        
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@fk_userID", userID);
            
            _conn.Open();
            bool ret = (await command.ExecuteNonQueryAsync()) == 1;
            _conn.Close();

            if (!ret)
            {
                return null;
            }

            return await GetCartByUserIDAsync(userID);
        }
    }

    public async Task<Cart?> GetCartByUserIDAsync(Guid? userID)
    {
        string sql = $"SELECT * FROM Carts WHERE fk_userID = @fk_userID";
        
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@fk_userID", userID);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                Cart c = new Cart(ret.GetGuid(0), (decimal)ret.GetSqlMoney(1), ret.GetInt32(2), ret.GetGuid(3));
                _conn.Close();
                return c;
            }

            _conn.Close();
            return null;
        }
    }

    public async Task<List<Product?>> GetAllProductsAsync()
    {
        string sql = $"SELECT productID, productName, productPrice, productDetails, stockDate, stock, dateCreated, dateModified FROM Products";
        List<Product?> productList = new List<Product?>();

        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            while (ret.Read())
            {
                Product product = new Product(
                    ret.GetGuid(0), 
                    ret.GetString(1), 
                    (decimal)ret.GetSqlMoney(2), 
                    ret.GetString(3), 
                    ret.GetDateTime(4), 
                    ret.GetInt32(5), 
                    ret.GetDateTime(6), 
                    ret.GetDateTime(7)
                );

                productList.Add(product);
            }

            _conn.Close();
            return productList;
        }
    }

    public async Task<Product?> GetProductByProductIDAsync(Guid? productID)
    {
        string sql = $"SELECT productID, productName, productPrice, productDetails, stockDate, stock, dateCreated, dateModified FROM Products WHERE productID = @productID";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@productID", productID);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                Product p = new Product(
                    ret.GetGuid(0), 
                    ret.GetString(1), 
                    (decimal)ret.GetSqlMoney(2), 
                    ret.GetString(3), 
                    ret.GetDateTime(4), 
                    ret.GetInt32(5), 
                    ret.GetDateTime(6), 
                    ret.GetDateTime(7)
                );

                _conn.Close();
                return p;
            }

            _conn.Close();
            return null;
        }
    }

    public async Task<Stream?> GetProductImageByProductIDAsync(Guid? productID)
    {
        string sql = $"SELECT productImage FROM Products WHERE productID = @productID";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@productID", productID);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                Stream photo = ret.GetStream(0);
                _conn.Close();
                return photo;
            }

            _conn.Close();
            return null;
        }
    }

    public async Task<List<Product?>> GetProductsFromCartAsync(Guid? cartID)
    {
        List<Guid?> productIDList = new List<Guid?>();
        List<Product?> productList = new List<Product?>();

        string sql = $"SELECT fk_productID FROM CartsProducts WHERE fk_cartID = @cartID";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@cartID", cartID);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            while (ret.Read())
            {
                productIDList.Add(ret.GetGuid(0));
            }

            _conn.Close();
        }

        foreach(Guid? productID in productIDList)
        {
            Product? p = await GetProductByProductIDAsync(productID);
            productList.Add(p); 
        }

        return productList;
    }

    public async Task<Order?> InsertOrderAsync(Guid orderID, Guid? userID, decimal? orderTotal)
    {
        string sql = $"INSERT INTO Orders (orderID, orderTotal, fk_userID) VALUES (@orderID, @orderTotal, @fk_userID)";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@orderID", orderID);
            command.Parameters.AddWithValue("@orderTotal", orderTotal);
            command.Parameters.AddWithValue("@fk_userID", userID);
            
            _conn.Open();
            bool ret = (await command.ExecuteNonQueryAsync()) == 1;
            _conn.Close();

            if (!ret)
            {
                return null;
            }

            return await GetOrderByOrderIDAsync(orderID);
        }
    }

    public async Task<Order?> GetOrderByOrderIDAsync(Guid? orderID)
    {
        string sql = $"SELECT * FROM Orders WHERE orderID = @orderID";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@orderID", orderID);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                DateTime? dateDelivered = null;
                bool? cancelled = null;
                bool? refunded = null;
                
                if(!(await ret.IsDBNullAsync(3)))
                {
                    dateDelivered = ret.GetDateTime(3);
                }

                if(!(await ret.IsDBNullAsync(4)))
                {
                    cancelled = ret.GetBoolean(4);
                }

                if(!(await ret.IsDBNullAsync(5)))
                {
                    cancelled = ret.GetBoolean(5);
                }

                Order o = new Order(
                    ret.GetGuid(0), 
                    (decimal)ret.GetSqlMoney(1), 
                    ret.GetDateTime(2), 
                    dateDelivered, 
                    cancelled, 
                    refunded,
                    ret.GetGuid(6) 
                );

                _conn.Close();
                return o;
            }

            _conn.Close();
            return null;
        }
    }

    public async Task<bool> InsertOrdersProductsAsync(List<Guid?> productIDs, Guid? orderID)
    {
        foreach(Guid? productID in productIDs)
        {
            string sql = $"INSERT INTO OrdersProducts (fk_productID, fk_orderID) Values (@fk_productID, @fk_orderID)";
            using (SqlCommand command = new SqlCommand(sql, _conn))
            {
                _conn.Open();
                command.Parameters.AddWithValue("@fk_productID", productID);
                command.Parameters.AddWithValue("@fk_orderID", orderID);
                
                await command.ExecuteNonQueryAsync();
                _conn.Close();
            }
        }

        return true;
    }

    public async Task<bool> DeleteAllItemsFromCartByCartIDAsync(Guid? cartID)
    {
        string sql1 = $"DELETE FROM CartsProducts WHERE fk_cartID = @cartID";
        string sql2 = $"UPDATE Carts SET cartItems = 0 WHERE cartID = @cartID";
        bool ret1 = false;
        bool ret2 = false;

        using (SqlCommand command = new SqlCommand(sql1, _conn))
        {
            command.Parameters.AddWithValue("@cartID", cartID);

            _conn.Open();
            ret1 = (await command.ExecuteNonQueryAsync()) > 0;
            _conn.Close();
        }

        using (SqlCommand command = new SqlCommand(sql2, _conn))
        {
            command.Parameters.AddWithValue("@cartID", cartID);

            _conn.Open();
            ret2 = (await command.ExecuteNonQueryAsync()) > 0;
            _conn.Close();
        }

        return ret1 && ret2;
    }

    public async Task<List<Order?>> GetMyOrdersAsync(Guid? userID)
    {
        string sql = $"SELECT * FROM Orders WHERE fk_userID = @userID";
        List<Order?> orderList = new List<Order?>();

        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@userID", userID);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            while (ret.Read())
            {
                DateTime? dateDelivered = null;

                if(!(await ret.IsDBNullAsync(3)))
                {
                    dateDelivered = ret.GetDateTime(3);
                }
                
                Order order = new Order(
                    ret.GetGuid(0), 
                    (decimal)ret.GetSqlMoney(1), 
                    ret.GetDateTime(2), 
                    dateDelivered, 
                    ret.GetBoolean(4), 
                    ret.GetBoolean(5),
                    ret.GetGuid(6) 
                );

                orderList.Add(order);
            }

            _conn.Close();
            return orderList;
        }
    }

    public async Task<List<Product?>> GetProductsInOrderAsync(Guid? orderID)
    {
        string sql = "SELECT fk_productID FROM OrdersProducts WHERE fk_orderID = @orderID";
        List<Product?> productList = new List<Product?>();
        List<Guid?> productIDList = new List<Guid?>();

        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@orderID", orderID);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            while (ret.Read())
            {
                productIDList.Add(ret.GetGuid(0));
            }

            _conn.Close();

            foreach(Guid? productID in productIDList)
            {
                productList.Add(await GetProductByProductIDAsync(productID));
            }
            
            return productList;
        }
    }
}
