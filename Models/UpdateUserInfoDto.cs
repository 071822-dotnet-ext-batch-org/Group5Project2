using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class UpdateUserInfoDto
    {
        public UpdateUserInfoDto()
        {
        }

        public UpdateUserInfoDto(string? address, string? phone)
        {
            Address = address;
            Phone = phone;
        }

        public string? Address {get; set;}
        public string? Phone {get; set;}
    }
}