using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Models
{
    public class RegisterDto
    {
        public RegisterDto()
        {
        }

        public RegisterDto(string? username, string? password, string? profileName, string? profileAddress, string? profilePhone, string? profileEmail, IFormFile? profilePicture)
        {
            Username = username;
            Password = password;
            ProfileName = profileName;
            ProfileAddress = profileAddress;
            ProfilePhone = profilePhone;
            ProfileEmail = profileEmail;
        }

        public string? Username {get; set;}
        public string? Password {get; set;}
        public string? ProfileName { get; set; }
        public string? ProfileAddress { get; set; }
        public string? ProfilePhone { get; set; }
        public string? ProfileEmail { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? ErrorMessage = string.Empty;
    }
}