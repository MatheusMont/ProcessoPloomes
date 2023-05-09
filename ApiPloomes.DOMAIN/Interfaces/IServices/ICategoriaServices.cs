using ApiPloomes.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Interfaces.IServices
{
    public interface ICategoriaServices
    {
        Task CreateCategoria(Categoria categoria);
        Task<Categoria> GetCategoriaById(Guid id);
        Task<Categoria> GetCategoriaByName(string name);
        Task UpdateCategoria(Categoria categoria, Guid id);
        Task DeleteCategoria(Guid id);
        Task<List<Categoria>> GetAllCategorias();
    }
}
