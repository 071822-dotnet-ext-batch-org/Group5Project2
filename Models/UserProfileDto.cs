using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserProfileDto 
    {
        public Guid ProfileID { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfileAddress { get; set; }
        public string? ProfilePhone { get; set; }
        public string? ProfileEmail { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public Guid Fk_UserID { get; set; }

    }
}
