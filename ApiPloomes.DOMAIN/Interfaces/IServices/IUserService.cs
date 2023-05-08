using ApiPloomes.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Interfaces.IServices
{
    public interface IUserServices
    {
        Task CreateUser(User user);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task UpdateUser(User user, Guid id);
        Task DeleteUser(Guid id);
        Task ChangePassword(User user, Guid id);
    }
}
