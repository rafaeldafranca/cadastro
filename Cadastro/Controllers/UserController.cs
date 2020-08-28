using Cadastro.Controllers.Base;
using Cadastro.Domain.Interfaces.Services;
using Cadastro.Domain.Models;
using Cadastro.Services;
using Cadastro.Services.Adapters;
using Cadastro.Services.Configs;
using Cadastro.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Cadastro.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userSrv;
        public UserController(IUserService userService)
        {
            _userSrv = userService;
        }

        /// <summary>
        /// Cria um usuário novo
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signingConfigurations"></param>
        /// <param name="tokenConfig"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult NewUser([FromBody] UserModel data
                                  , [FromServices] SigningConfig signingConfigurations
                                  , [FromServices] TokenConfig tokenConfig)
        {
            return ReturnPackage(() =>
            {
                if (_userSrv.CheckUser(data.Email))
                    return StatusCode(400, "E-mail já existente");

                var currentUser = _userSrv.Add(data.Adapter());
                var currentLogin = new LoginModel(currentUser.Id, currentUser.Name, currentUser.Email
                    , currentUser.Created, currentUser.Modified, currentUser.Last_login,
                    currentUser.Phones.Select(q => new PhoneUserModel()
                    {
                        Ddd = q.Ddd,
                        Number = q.Number
                    }).ToList());

                TokenModel token = new TokenService().Token(
                                                currentLogin,
                                                signingConfigurations,
                                                tokenConfig);

                currentLogin.Token = token.AccessToken;

                return currentLogin;
            }
            , System.Net.HttpStatusCode.Created);

        }

        /// <summary>
        /// Buscar todos os dados do banco.
        /// </summary>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return ReturnPackage(() => _userSrv.GetAll());
        }

        /// <summary>
        /// Buscar usuario corrente buscando por parâmetro.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet("Profile/{id}")]
        public IActionResult GetById(Guid id)
        {
            return ReturnPackage(() =>
            {
                var TokenUserId = Guid.Parse(User.Identity.Name);
                if (id != TokenUserId)
                    return StatusCode(401, new { mensagem = "Não autorizado" });

                return Get();
            });
        }

        /// <summary>
        /// Buscar usuario utilizando o token
        /// </summary>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet("Profile")]
        public IActionResult Get()
        {
            return ReturnPackage(() =>
            {
                Guid TokenUserId = Guid.Parse(User.Identity.Name);
                return _userSrv.GetbyId(TokenUserId)?.Adapter();
            });
        }
    }
}