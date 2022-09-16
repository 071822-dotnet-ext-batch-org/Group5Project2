using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class LoginDto
    {
        public string? Username {get; set;}
        public string? Password {get; set;}
        public string? ErrorMessage = string.Empty;

        public LoginDto (string? username, string? password) 
        {
            this.Username = username;
            this.Password = password;
        }
    }
}