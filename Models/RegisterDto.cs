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

        public RegisterDto(string? userID, string? profileName, string? profileEmail, string? profilePicture, string? profilePhone, string? profileAddress)
        {
            UserID = userID;
            ProfileName = profileName;
            ProfileEmail = profileEmail;
            ProfilePicture = profilePicture;
            ProfilePhone = profilePhone;
            ProfileAddress = profileAddress;
        }

        public string? UserID { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfileEmail { get; set; }
        public string? ProfilePicture { get; set; }
        public string? ProfileAddress { get; set; }
        public string? ProfilePhone { get; set; }
        public string? ErrorMessage = string.Empty;
    }
}