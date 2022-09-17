using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class UserInfoDto
    {
        public UserInfoDto()
        {
        }

        public UserInfoDto(string? profileName, string? profileAddress, string? profilePhone, string? profileEmail, Cart? cart, string? profilePicture)
        {
            ProfileName = profileName;
            ProfileAddress = profileAddress;
            ProfilePhone = profilePhone;
            ProfileEmail = profileEmail;
            ProfilePicture = profilePicture;
            Cart = cart;
        }
        public string? ProfileName {get; set;}
        public string? ProfileAddress {get; set;}
        public string? ProfilePhone {get; set;}
        public string? ProfileEmail {get; set;}
        public string? ProfilePicture {get; set;} 
        public Cart? Cart {get; set;} 
        public string? ErrorMessage {get; set;} = string.Empty;
    }
}