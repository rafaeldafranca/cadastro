using Cadastro.Controllers.Base;
using Cadastro.Domain.Adapters;
using Cadastro.Domain.Entities;
using Cadastro.Domain.Interfaces.Services;
using Cadastro.Domain.VO;
using Cadastro.Services;
using Cadastro.Services.Configs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cadastro.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userSrv;
        public AuthController(IUserService userService)
        {
            _userSrv = userService;
        }

        /// <summary>
        /// Gera o token autenticando o usuário.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signingConfigurations"></param>
        /// <param name="tokenConfig"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IActionResult), 200)]
        [AllowAnonymous]
        [HttpPost("Token")]
        public IActionResult Token([FromBody] AuthModel data
                                  , [FromServices] SigningConfig signingConfigurations
                                  , [FromServices] TokenConfig tokenConfig)
        {
            try
            {
                bool UserExist = _userSrv.CheckUser(data.Email);
                if (!UserExist)
                    return ReturnPackage(() => null,
                           System.Net.HttpStatusCode.BadRequest, "Usuario Inválido");
                else
                {
                    User currentUser = _userSrv.Validate(data.Email, data.Password);
                    if (currentUser == null)
                    {
                        return ReturnPackage(() => null,
                               System.Net.HttpStatusCode.Unauthorized, "Usuario Inválido");
                    }
                    else
                    {
                        //retorna na estrutura do token.
                        //return ReturnPackage(() =>
                        //    new TokenService().Token(currentUser, signingConfigurations, tokenConfig)
                        //);

                        //retorna na estrutura do desafio.
                        return ReturnPackage(() =>
                        {
                            var token = new TokenService().Token(currentUser, signingConfigurations, tokenConfig);
                            var user = currentUser.Adapter();
                            user.Token = token.AccessToken;
                            return user;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = ex.Message });
            }
        }
    }
}
