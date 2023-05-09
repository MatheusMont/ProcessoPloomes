using ApiPloomes.API.Configurations;
using ApiPloomes.DOMAIN.DTOs.Request;
using ApiPloomes.DOMAIN.DTOs.Response;
using ApiPloomes.DOMAIN.Interfaces.INotifier;
using ApiPloomes.DOMAIN.Interfaces.IServices;
using ApiPloomes.DOMAIN.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPloomes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : BaseController
    {
        private readonly IProdutoServices _produtoServices;
        private readonly INotifier _notifier;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoServices produtoServices,
                                    INotifier notifier,
                                    IMapper mapper) : base(notifier, mapper)
        {
            _produtoServices = produtoServices;
            _notifier = notifier;
            _mapper = mapper;
        }

        /// <summary>
        /// Retornar o produto a partir de seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um erro ou as informações públicas do produto.</returns>
        /// <response code = "201">Os dados públicos do produto buscado.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProdutoById(Guid id)
        {
            var produto = await _produtoServices.GetProdutoById(id);

            return HasError()
                ? ReturnBadRequest()
                : Ok(_mapper.Map<ProdutoResponse>(produto));
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="produtoDto"></param>
        /// <returns>Uma mensagem indicando sucesso ou os erros que impedem a criação.</returns>
        /// <response code = "201">Retorna uma mensagem de sucesso de criação de produto.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduto(ProdutoRequest produtoDto)
        {
            /*
            var produto = new Produto()
            {
                Nome = produtoDto.Nome,
                Preco = produtoDto.Preco,
                Descricao = produtoDto.Descricao,
                CategoriaId = produtoDto.CategoriaId,
                UsuarioId = produtoDto.UsuarioId
            };
            */
            var produto = _mapper.Map<Produto>(produtoDto); 

            await _produtoServices.CreateProduto(produto);

            return HasError()
                ? ReturnBadRequest()
                : StatusCode(201, "Created");
        }

        /// <summary>
        /// Atualiza os dados públicos do produto.
        /// </summary>
        /// <param name="produtoDto"></param>
        /// <returns>Confirmação da execução ou os erros que impediram.</returns>
        /// <response code = "201">Retorna uma mensagem de sucesso de atualização do produto.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpPut("Update/{id:Guid}")]
        public async Task<IActionResult> UpdateProduto([FromBody] ProdutoRequest produtoDto, Guid id)
        {
            var produto = _mapper.Map<Produto>(produtoDto);

            await _produtoServices.UpdateProduto(produto, id);

            return HasError()
                ? ReturnBadRequest()
                : Ok("Produto atualizado com sucesso");
        }

        /// <summary>
        /// Remove o produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Confirmação da execução ou os erros que impediram.</returns>
        /// <response code = "201">Retorna uma mensagem de sucesso de deleção de produto.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpDelete("Delete/{id:Guid}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            await _produtoServices.DeleteProduto(id);

            return HasError()
                ? ReturnBadRequest()
                : Ok("Produto deletado com sucesso");
        }

        /// <summary>
        /// Retorna todos os dados públicos de todos os produtos.
        /// </summary>
        /// <returns>Um erro ou as informações dos produtos.</returns>
        /// <response code = "201">Retorna todos os dados públicos de todos os produtos.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("All")]
        public async Task<IActionResult> GetAllProdutos()
        {
            var produtos = await _produtoServices.GetAllProdutos();

            return HasError()
                ? ReturnBadRequest()
                : Ok(_mapper.Map<List<ProdutoResponse>>(produtos));
        }

        /// <summary>
        /// Retorna todos os dados públicos de todos os produtos da categoria informada.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um erro ou as informações dos produtos.</returns>
        /// <response code = "201">Retorna todos os dados públicos de todos os produtos da categoria informada.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("Categoria/{id:Guid}")]
        public async Task<IActionResult> GetAllProdutosFromCategoria(Guid id)
        {
            var produtos = await _produtoServices.GetAllProdutosByCategoria(id);

            return HasError()
                ? ReturnBadRequest()
                : Ok(_mapper.Map<List<ProdutoResponse>>(produtos));
        }

        /// <summary>
        /// Retorna todos os dados públicos de todos os produtos do usuário informado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um erro ou as informações dos produtos.</returns>
        /// <response code = "201">Retorna todos os dados públicos de todos os produtos do usuário informado.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("User/{id:Guid}")]
        public async Task<IActionResult> GetAllProdutosFromUser(Guid id)
        {
            var produtos = await _produtoServices.GetAllProdutosByUser(id);

            return HasError()
                ? ReturnBadRequest()
                : Ok(_mapper.Map<List<ProdutoResponse>>(produtos));
        }

        /// <summary>
        /// Apenas para testes, retorna todos os dados de todos os produtos.
        /// </summary>
        /// <returns>Um erro ou as informações dos produtos.</returns>
        /// <response code = "201">Retorna todos os dados públicos de todos os produtos.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("Masster/All")]
        public async Task<IActionResult> GetAllProdutosMaster()
        {
            var produtos = await _produtoServices.GetAllProdutos();

            return HasError()
                ? ReturnBadRequest()
                : Ok(produtos);
        }
    }
}
