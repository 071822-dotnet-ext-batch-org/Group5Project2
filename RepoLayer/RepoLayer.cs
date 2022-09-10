using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;
using System.Data;

namespace RepoLayer;

public class Repo : IRepo
{
    private readonly IConfiguration _config;

    public Repo(IConfiguration config)
    {
        _config = config;
    }

    public async Task<User?> GetUserByUsername(string? username)
    {
        SqlConnection conn = new SqlConnection(_config["ConnectionStrings:project2ApiDB"]);
        using (SqlCommand command = new SqlCommand($"SELECT * FROM [dbo].[Users] WHERE username = @username", conn))
        {
            command.Parameters.AddWithValue("@username", username);
            conn.Open();

            SqlDataReader? ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                User u = new User(ret.GetGuid(0), ret.GetString(1), ret.GetString(2), ret.GetDateTime(3), ret.GetDateTime(4));
                conn.Close();
                return u;
            }

            conn.Close();
            return null;
        }

    }
}
