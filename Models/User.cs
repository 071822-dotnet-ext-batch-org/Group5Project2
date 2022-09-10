using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public Guid? UserID {get; set;}
        public string? Username {get; set;}
        public string? Password {get; set;}
        public DateTime? DateCreated {get; set;}
        public DateTime? DateModified {get; set;}

        public User (Guid? userID, string? username, string? password, DateTime? dateCreated, DateTime? dateModified) 
        {
            this.UserID = userID;
            this.Username = username;
            this.Password = password;
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
        }

    }
}