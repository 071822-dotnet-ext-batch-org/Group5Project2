using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public string? UserID {get; set;}
        public DateTime? DateCreated {get; set;}
        public DateTime? DateModified {get; set;}

        public User (string? userID, DateTime? dateCreated, DateTime? dateModified) 
        {
            this.UserID = userID;
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
        }

    }
}