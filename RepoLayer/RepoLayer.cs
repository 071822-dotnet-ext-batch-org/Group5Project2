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

    public async Task<User?> GetUserByUsername(string? username)
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

    public async Task<UserProfile?> GetProfileByUserID(Guid? userID)
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

    public async Task<Stream?> GetProfilePictureByUserID(Guid? userID)
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

            return await GetUserByUsername(request.Username);
        }
    }


    public async Task<UserProfile?> InsertProfileAsync(RegisterDto request)
    {
        string sql = $"INSERT INTO Profiles (profileName, profileAddress, profilePhone, profileEmail, profilePicture, fk_userID) VALUES (@profileName, @profileAddress, @profilePhone, @profileEmail, @profilePicture, @fk_userID)";
        Guid? userID = (await GetUserByUsername(request.Username))?.UserID;
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

            return await GetProfileByUserID(userID);
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

            return await GetCartByUserID(userID);
        }
    }

    public async Task<Cart?> GetCartByUserID(Guid? userID)
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
}
