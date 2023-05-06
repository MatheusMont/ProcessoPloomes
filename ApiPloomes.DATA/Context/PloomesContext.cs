using ApiPloomes.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DATA.Context
{
    public class PloomesContext : DbContext
    {
        public PloomesContext(DbContextOptions<PloomesContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
