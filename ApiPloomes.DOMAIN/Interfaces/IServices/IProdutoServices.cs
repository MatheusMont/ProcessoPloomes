using ApiPloomes.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Interfaces.IServices
{
    public interface IProdutoServices
    {
        Task CreateProduto(Produto produto);
        Task<Produto> GetProdutoById(Guid id);
        Task UpdateProduto(Produto produto, Guid id);
        Task DeleteProduto(Guid id);
        Task<List<Produto>> GetAllProdutos();
        Task<List<Produto>> GetAllProdutosByUser(Guid id);
        Task<List<Produto>> GetAllProdutosByCategoria(Guid id);
    }
}
