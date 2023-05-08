using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.DTOs.Response
{
    public class UserResponse
    {
        public string Username { get; private set; }
        public string Email { get; private set; }

        public UserResponse(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
