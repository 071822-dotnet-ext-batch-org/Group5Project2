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
}
