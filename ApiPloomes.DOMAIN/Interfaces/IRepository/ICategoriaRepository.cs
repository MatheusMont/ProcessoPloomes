using ApiPloomes.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Interfaces.IRepository
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
        Task<Categoria> GetByName(string name);
    }
}
