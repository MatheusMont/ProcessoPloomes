using ApiPloomes.DATA.Context;
using ApiPloomes.DOMAIN.Interfaces.IRepository;
using ApiPloomes.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DATA.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly PloomesContext _context;

        public ProdutoRepository(PloomesContext context) : base(context) 
        {
            _context = context;
        }


        public async Task<List<Produto>> GetAllFromCategoria(Guid id)
        {
            return _context.Produtos.Include(p => p.Usuario)
                                    .Include(p => p.Categoria)
                                    .Where(p => p.CategoriaId == id && p.Active == true).ToList();
        }

        public async Task<List<Produto>> GetAllFromUser(Guid id)
        {
            return _context.Produtos.Include(p => p.Usuario)
                                    .Include(p => p.Categoria)
                                    .Where(p => p.UsuarioId == id && p.Active == true).ToList();
        }

        public async Task<List<Produto>> GetAll()
        {
            return _context.Produtos.Include(p => p.Usuario)
                                    .Include(p => p.Categoria)
                                    .Where(p => p.Active == true).ToList();
        }

        public async Task<Produto> GetById(Guid id)
        {
            return _context.Produtos.Include(p => p.Usuario)
                                    .Include(p => p.Categoria)
                                    .FirstOrDefault(p => p.Id == id && p.Active == true);
        }
    }
}
