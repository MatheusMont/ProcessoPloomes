using ApiPloomes.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Interfaces.IRepository
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task Create(T entity);
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        void Update(T entity);
        Task Delete(Guid id);
        Task<int> Save();
    }
}
