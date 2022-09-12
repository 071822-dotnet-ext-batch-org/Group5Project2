using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserProfile
    {

        public UserProfile()
        {
        }

        public UserProfile(Guid? profileID, string? profileName, string? profileAddress, string? profilePhone, string? profileEmail, Stream? profilePicture, Guid? fk_UserID, DateTime? dateCreated, DateTime? dateModified)

        {
            ProfileID = profileID;
            ProfileName = profileName;
            ProfileAddress = profileAddress;
            ProfilePhone = profilePhone;
            ProfileEmail = profileEmail;
            ProfilePicture = profilePicture;
            Fk_UserID = fk_UserID;
            this.dateCreated = dateCreated;
            this.dateModified = dateModified;
        }

        

        public Guid?  ProfileID { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfileAddress { get; set; }
        public string? ProfilePhone { get; set; }
        public string? ProfileEmail { get; set; }
        public Stream? ProfilePicture { get; set; }
        public Guid? Fk_UserID { get; set; }

        public DateTime? dateCreated {get; set;}
        public DateTime? dateModified {get; set;}
    }


}
