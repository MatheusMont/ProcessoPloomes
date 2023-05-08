using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.DTOs.Request
{
    public class UserUpdateRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public UserUpdateRequest(string username, string email)
        {
            Username = username;
            Email = email;
        }

        public UserUpdateRequest() { }
    }
}
