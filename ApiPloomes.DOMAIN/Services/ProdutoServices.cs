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
    public class ProdutoServices : BaseService, IProdutoServices
    {
        private readonly IProdutoRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly INotifier _notifier;

        public ProdutoServices(IProdutoRepository repository,
                                IUserRepository userRepository,
                                ICategoriaRepository categoriaRepository,
                                INotifier notifier) : base(notifier)
        {
            _repository = repository;
            _userRepository = userRepository;
            _categoriaRepository = categoriaRepository;
            _notifier = notifier;
        }

        public async Task CreateProduto(Produto produto)
        {
            try
            {
                var validation = ExecuteValidation(new ProdutoValidator(), produto);

                if (!validation)
                    return;

                var usuario = await _userRepository.GetById(produto.UsuarioId);
                var categoria = await _categoriaRepository.GetById(produto.CategoriaId);

                if (usuario == null)
                    NotifyErrorMessage("Usuario", "O usuário informado não existe");

                if (categoria == null)
                    NotifyErrorMessage("Categoria", "A categoria informada não existe");

                //produto.Usuario = usuario;
                //produto.Categoria = categoria;

                if (!_notifier.HasNotifications())
                {
                    await _repository.Create(produto);
                    await _repository.Save();
                }

            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
            }
        }

        public async Task DeleteProduto(Guid id)
        {
            try
            {
                if (await _repository.Exists(id) == false)
                {
                    NotifyErrorMessage("ID", "O produto não existe");
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

        public async Task<List<Produto>> GetAllProdutos()
        {
            try
            {
                var produtos = await _repository.GetAll();

                if (produtos == null)
                    NotifyErrorMessage("Produto", "Não existem produtos");

                return produtos;
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
                return new List<Produto>();
            }
        }

        public async Task<List<Produto>> GetAllProdutosByCategoria(Guid id)
        {
            try
            {
                var produtos = await _repository.GetAllFromCategoria(id);

                if (produtos == null)
                    NotifyErrorMessage("Produto", "Não existem produtos desta categoria");

                return produtos;
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
                return new List<Produto>();
            }
        }

        public async Task<List<Produto>> GetAllProdutosByUser(Guid id)
        {
            try
            {
                var produtos = await _repository.GetAllFromUser(id);

                if (produtos == null)
                    NotifyErrorMessage("Produto", "Não existem produtos deste usuário");

                return produtos;
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
                return new List<Produto>();
            }
        }

        public async Task<Produto> GetProdutoById(Guid id)
        {
            try
            {
                var user = await _repository.GetById(id);

                if (user == null)
                    NotifyErrorMessage("Email", "Este produto não está cadastrado");

                return user;
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde.");
                return new Produto();
            }
        }

        public async Task UpdateProduto(Produto produto, Guid id)
        {
            try
            {
                if (await _repository.Exists(id) == false)
                {
                    NotifyErrorMessage("ID", "O usuário não existe");
                    return;
                }

                var validation = ExecuteValidation(new ProdutoValidator(), produto);

                if (!validation)
                    return;

                var updateProduto = await _repository.GetById(id);

                updateProduto.CategoriaId = produto.CategoriaId;
                updateProduto.Descricao = produto.Descricao;
                updateProduto.Nome = produto.Nome;
                updateProduto.UsuarioId = produto.UsuarioId;
                updateProduto.Preco = produto.Preco;
                updateProduto.UpdateDate = DateTime.UtcNow;

                _repository.Update(updateProduto);
                await _repository.Save();
            }
            catch (Exception e)
            {
                NotifyErrorMessage($"{e}", "Ocorreu um erro inesperado, tente novamente mais tarde");
            }
        }
    }
}
