using ApiPloomes.DATA.Context;
using ApiPloomes.DOMAIN.Interfaces.IRepository;
using ApiPloomes.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DATA.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly PloomesContext _context;

        public UserRepository(PloomesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> EmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email && u.Active == true);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Active == true);
        }
    }
}
