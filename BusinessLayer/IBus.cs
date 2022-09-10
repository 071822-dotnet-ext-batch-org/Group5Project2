using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace BusinessLayer
{
    public interface IBus
    {
        Task<User?> LoginAsync(LoginDto request);
    }
}