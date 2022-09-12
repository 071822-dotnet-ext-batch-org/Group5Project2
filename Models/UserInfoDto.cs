using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class UserInfoDto
    {
        public UserInfoDto() { }
        public UserInfoDto(Guid? userID, string? username, Guid? profileID, string? profileName, string? profileAddress, string? profilePhone, string? profileEmail)
        {
            UserID = userID;
            Username = username;
            ProfileID = profileID;
            ProfileName = profileName;
            ProfileAddress = profileAddress;
            ProfilePhone = profilePhone;
            ProfileEmail = profileEmail;
        }

        public Guid? UserID {get; set;}
        public string? Username {get; set;}
        public Guid? ProfileID {get; set;}
        public string? ProfileName {get; set;}
        public string? ProfileAddress {get; set;}
        public string? ProfilePhone {get; set;}
        public string? ProfileEmail {get; set;}    
    }
}