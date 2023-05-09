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
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        private readonly PloomesContext _context;

        public CategoriaRepository(PloomesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Categoria?> GetByName(string name)
        {
            return _context.Categorias.FirstOrDefault(c => c.Nome == name && c.Active == true);
        }
    }
}
