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
        string sql = $"SELECT * FROM [dbo].[Users] WHERE username = @username";
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
        
        string sql = $"SELECT profileID, profileName, profileAddress, profilePhone, profileEmail FROM [dbo].[Profiles] WHERE fk_userID = @userID";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@userID", userID);
            _conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                UserProfile p = new UserProfile(ret.GetGuid(0), ret.GetString(1), ret.GetString(2), ret.GetString(3), ret.GetString(4), null, null, null, null);
                return p;
            }

            _conn.Close();
            return null;
        }
    }

    public async Task<byte[]?> GetProfilePictureByUserID(Guid? userID)
    {
        int bufferSize = 100;
        byte[] outByte = new byte[bufferSize];
        string sql = $"SELECT profilePicture FROM [dbo].[Profiles] WHERE fk_userID = @userID";
        using (SqlCommand command = new SqlCommand(sql, _conn))
        {
            command.Parameters.AddWithValue("@userID", userID);
            _conn.Open();

            // SqlDataReader? ret = await command.ExecuteReaderAsync();
            // if (ret.Read())
            // {
            //     ret.GetBytes(0, 0, outByte, 0, bufferSize);

            //     return outByte;
            // }

            byte[]? photo = (byte[]?)(await command.ExecuteScalarAsync());
            _conn.Close();
            return photo;
        }
    }
}
