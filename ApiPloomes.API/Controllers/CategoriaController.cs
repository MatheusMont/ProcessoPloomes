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
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaServices _categoriaServices;
        private readonly INotifier _notifier;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaServices categoriaServices,
                                INotifier notifier,
                                IMapper mapper) : base(notifier, mapper)
        {
            _categoriaServices = categoriaServices;
            _notifier = notifier;
            _mapper = mapper;
        }

        /// <summary>
        /// Retornar a categoria a partir de seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um erro ou o nome da categoria.</returns>
        /// <response code = "201">Retorna o nome da categoria buscada.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoriaById(Guid id)
        {
            var categoria = await _categoriaServices.GetCategoriaById(id);

            return HasError()
                ? ReturnBadRequest()
                : Ok(_mapper.Map<CategoriaResponse>(categoria));
        }

        /// <summary>
        /// Retorna a categoria a partir do nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Um erro ou o nome da categoria.</returns>
        /// <response code = "201">Retorna o nome da categoria buscada.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("{nome}")]
        public async Task<IActionResult> GetCategoriaByNome(string nome)
        {
            var categoria = await _categoriaServices.GetCategoriaByName(nome);

            return HasError()
                ? ReturnBadRequest()
                : Ok(_mapper.Map<CategoriaResponse>(categoria));
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="categoriaDto"></param>
        /// <returns>Uma mensagem indicando sucesso ou os erros que impedem a criação.</returns>
        /// <response code = "201">Retorna uma mensagem de sucesso de criação de usuário.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategoria(CategoriaRequest categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            await _categoriaServices.CreateCategoria(categoria);

            return HasError()
                ? ReturnBadRequest()
                : StatusCode(201, "Created");
        }

        /// <summary>
        /// Atualiza os dados públicos da categoria.
        /// </summary>
        /// <param name="categoriaDto"></param>
        /// <returns>Confirmação da execução ou os erros que impediram.</returns>
        /// <response code = "201">Retorna uma mensagem de sucesso de atualização da categoria.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpPut("Update/{id:Guid}")]
        public async Task<IActionResult> UpdateCategoria([FromBody] CategoriaRequest categoriaDto, Guid id)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            await _categoriaServices.UpdateCategoria(categoria, id);

            return HasError()
                ? ReturnBadRequest()
                : Ok("Categoria atualizada com sucesso");
        }

        /// <summary>
        /// Remove o usuário
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Confirmação da execução ou os erros que impediram.</returns>
        /// <response code = "201">Retorna uma mensagem de sucesso de deleção da categoria.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpDelete("Delete/{id:Guid}")]
        public async Task<IActionResult> DeleteCategoria(Guid id)
        {
            await _categoriaServices.DeleteCategoria(id);

            return HasError()
                ? ReturnBadRequest()
                : Ok("Categoria deletada com sucesso");
        }

        /// <summary>
        /// Retorna os dados públicos de todas as categorias registradas.
        /// </summary>
        /// <returns>Um erro ou as informações do usuário.</returns>
        /// <response code = "201">Retorna uma lista com todas as categorias.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategorias()
        {
            var categorias = await _categoriaServices.GetAllCategorias();

            return HasError()
                ? ReturnBadRequest()
                : Ok(_mapper.Map<List<CategoriaResponse>>(categorias));
        }

        /// <summary>
        /// Apenas para testes, retorna todos os dados de todas as categorias
        /// </summary>
        /// <returns>Uma lista com todos os dados de todas as categorias registradas</returns>
        [HttpGet("master/all")]
        public async Task<IActionResult> GetAllCategoriasMaster()
        {
            var categorias = await _categoriaServices.GetAllCategorias();

            return HasError()
                ? ReturnBadRequest()
                : Ok(categorias);
        }

        /// <summary>
        /// Apenas para testes, retorna todos os dados da Categoria partir do nome para que se possa verificar o ID
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Um erro ou todas as informações da categoria buscada.</returns>
        [HttpGet("master/{nome}")]
        public async Task<IActionResult> GetCategoriaByNomeMaster(string nome)
        {
            var categoria = await _categoriaServices.GetCategoriaByName(nome);

            return HasError()
                ? ReturnBadRequest()
                : Ok(categoria);
        }
    }
}
