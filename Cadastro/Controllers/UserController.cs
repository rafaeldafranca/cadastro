using Cadastro.Controllers.Base;
using Cadastro.Domain.Adapters;
using Cadastro.Domain.Interfaces.Services;
using Cadastro.Domain.VO;
using Cadastro.Services;
using Cadastro.Services.Configs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
            try
            {
                if (_userSrv.CheckUser(data.Email))
                    return StatusCode(400, "E-mail já existente");
                else
                {
                    var _newUser = data.Adapter();
                    return ReturnPackage(() =>
                    {
                        var userDB = _userSrv.Add(_newUser);
                        var user = userDB.Adapter();
                        var token = new TokenService().Token(userDB, signingConfigurations, tokenConfig);
                        user.Token = token.AccessToken;

                        return user;
                    }
                    , System.Net.HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = ex.Message });
            }

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
            try
            {
                var TokenUserId = Guid.Parse(User.Identity.Name);
                if (id != TokenUserId)
                    return StatusCode(401, new { mensagem = "Não autorizado" });

                return Get();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Buscar usuario utilizando o token
        /// </summary>
        /// <returns></returns>
        [Authorize("Bearer")]
        [HttpGet("Profile")]
        public IActionResult Get()
        {
            try
            {
                var TokenUserId = Guid.Parse(User.Identity.Name);
                return ReturnPackage(() => _userSrv.GetbyId(TokenUserId)?.Adapter());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = ex.Message });
            }
        }
    }
}
