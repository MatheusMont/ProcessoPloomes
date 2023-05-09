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
    public class UserController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly INotifier _notifier;
        private readonly IMapper _mapper;

        public UserController(IUserServices userServices,
                                INotifier notifier,
                                IMapper mapper) : base(notifier, mapper)
        {
            _userServices = userServices;
            _notifier = notifier;
            _mapper = mapper;
        }

        /// <summary>
        /// Retornar o usuário a partir de seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um erro ou as informações públicas do usuário.</returns>
        /// <response code = "201">Os dados públicos do usuário buscado.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userServices.GetUserById(id);

            return HasError()
                ? ReturnBadRequest()
                : Ok(_mapper.Map<UserResponse>(user));
        }

        /// <summary>
        /// Retorna o usuário a partir de seu email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Um erro ou as informações públicas do usuário.</returns>
        /// <response code = "201">Os dados públicos do usuário buscado.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userServices.GetUserByEmail(email);

            return HasError()
                ? ReturnBadRequest()
                : Ok(_mapper.Map<UserResponse>(user));
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Uma mensagem indicando sucesso ou os erros que impedem a criação.</returns>
        /// <response code = "201">Retorna uma mensagem de sucesso de criação de usuário.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(UserCreationRequest userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _userServices.CreateUser(user);

            return HasError()
                ? ReturnBadRequest()
                : StatusCode(201, "Created");
        }

        /// <summary>
        /// Atualiza os dados públicos do usuário.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Confirmação da execução ou os erros que impediram.</returns>
        /// <response code = "201">Retorna uma mensagem de sucesso de atualização do usuário.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpPut("Update/{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest userDto, Guid id)
        {
            var user = _mapper.Map<User>(userDto);

            await _userServices.UpdateUser(user, id);

            return HasError()
                ? ReturnBadRequest()
                : Ok("Usuário atualizado com sucesso");
        }

        /// <summary>
        /// Muda a senha do usuário
        /// </summary>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns>Confirmação da execução ou os erros que impediram.</returns>
        /// <response code = "201">Retorna uma mensagem de sucesso de alteração da senha do usuário.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpPut("Update/ChangePassword/{id:Guid}")]
        public async Task<IActionResult> ChangePassword([FromBody] string password, Guid id)
        {

            var userDto = new UserCreationRequest("UsernamePlaceHolder", password, "email@placeholder.com");
            var user = _mapper.Map<User>(userDto);
            await _userServices.ChangePassword(user, id);

            return HasError()
                ? ReturnBadRequest()
                : Ok("Usuário atualizado com sucesso");
        }

        /// <summary>
        /// Remove o usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Confirmação da execução ou os erros que impediram.</returns>
        /// <response code = "201">Retorna uma mensagem de sucesso de deleção de usuário.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpDelete("Delete/{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userServices.DeleteUser(id);

            return HasError()
                ? ReturnBadRequest()
                : Ok("Usuário deletado com sucesso");
        }

        /// <summary>
        /// Apenas para testes, retorna todos os dados do usuário a partir do email para que se possa verificar o ID
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Um erro ou as informações do usuário.</returns>
        /// <response code = "201">Retorna todos os dados do usuário buscado.</response>
        /// <response code = "400">Retorna uma ou mais mensagens de erro.</response>
        [HttpGet("master/{email}")]
        public async Task<IActionResult> GetUserByEmailMaster(string email)
        {
            var user = await _userServices.GetUserByEmail(email);

            return HasError()
                ? ReturnBadRequest()
                : Ok(user);
        }
    }
}
