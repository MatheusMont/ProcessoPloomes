using ApiPloomes.DOMAIN.Interfaces.INotifier;
using ApiPloomes.DOMAIN.Interfaces.IRepository;
using ApiPloomes.DOMAIN.Interfaces.IServices;
using ApiPloomes.DOMAIN.Models;
using ApiPloomes.DOMAIN.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Services
{
    public class CategoriaService : BaseService, ICategoriaServices
    {
        private readonly ICategoriaRepository _repository;
        private readonly INotifier _notifier;

        public CategoriaService(INotifier notifier,
                            ICategoriaRepository repository) : base(notifier)
        {
            _repository = repository;
            _notifier = notifier;
        }

        public async Task CreateCategoria(Categoria categoria)
        {
            try
            {
                var validation = ExecuteValidation(new CategoriaValidator(), categoria);

                if (!validation)
                    return;

                if (await _repository.GetByName(categoria.Nome) != null)
                    NotifyErrorMessage("Categoria", "Esta categoria já está cadastrada");

                if (!_notifier.HasNotifications())
                {
                    await _repository.Create(categoria);
                    await _repository.Save();
                }

            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
            }
        }

        public async Task DeleteCategoria(Guid id)
        {
            try
            {
                if (await _repository.Exists(id) == false)
                {
                    NotifyErrorMessage("ID", "A categoria não existe");
                    return;
                }

                await _repository.Delete(id);
                await _repository.Save();
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde");
            }
        }

        public async Task<List<Categoria>> GetAllCategorias()
        {
            try
            {
                var categorias = await _repository.GetAll();

                if (categorias == null)
                    NotifyErrorMessage("Categoria", "Não existem categorias");

                return categorias;
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
                return new List<Categoria>();
            }
        }

        public async Task<Categoria> GetCategoriaById(Guid id)
        {
            try
            {
                var categoria = await _repository.GetById(id);

                if (categoria == null)
                    NotifyErrorMessage("Categoria", "Este ID não está cadastrado");

                return categoria;
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
                return new Categoria();
            }
        }

        public async Task<Categoria> GetCategoriaByName(string name)
        {
            try
            {
                var categoria = await _repository.GetByName(name);

                if (categoria == null)
                    NotifyErrorMessage("Categoria", "Este ID não está cadastrado");

                return categoria;
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
                return new Categoria();
            }
        }

        public async Task UpdateCategoria(Categoria categoria, Guid id)
        {
            try
            {
                if (await _repository.Exists(id) == false)
                {
                    NotifyErrorMessage("ID", "A categoria não existe");
                    return;
                }

                var validation = ExecuteValidation(new CategoriaValidator(), categoria);

                if (!validation)
                    return;

                var updateCategoria = await _repository.GetById(id);

                updateCategoria.Nome = categoria.Nome;
                updateCategoria.UpdateDate = DateTime.UtcNow;

                _repository.Update(updateCategoria);
                await _repository.Save();
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde");
            }
        }
    }
}
