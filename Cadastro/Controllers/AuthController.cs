﻿using Cadastro.Controllers.Base;
using Cadastro.Domain.Interfaces.Services;
using Cadastro.Services;
using Cadastro.Services.Configs;
using Cadastro.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userSrv;
        public AuthController(IUserService userService) => _userSrv = userService;

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
            return ReturnPackage(() =>
            {
                bool UserExist = _userSrv.CheckUser(data.Email);
                if (!UserExist)
                    return StatusCode(400, "Usuario Inválido");

                var currentUser = _userSrv.Validate(data.Email, data.Password);
                if (currentUser == null)
                    return StatusCode(401, "Usuario Inválido");

                var token = new TokenService().Token(currentUser, signingConfigurations, tokenConfig);
                currentUser.Token = token.AccessToken;

                return currentUser;
            });
        }
    }
}