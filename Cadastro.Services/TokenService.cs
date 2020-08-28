using Cadastro.Domain.Entities;
using Cadastro.Services.Configs;
using Cadastro.Services.Interfaces;
using Cadastro.Services.Models;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Cadastro.Services
{
    public class TokenService: ITokenService
    {

        public TokenModel Token(User user, SigningConfig signingConfigurations
                                 , TokenConfig tokenConfigurations)
        {
            try
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Id.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                    }
                );

                DateTime CreatedDate = DateTime.Now;
                DateTime ExpiredDate = CreatedDate + TimeSpan.FromMinutes(tokenConfigurations.Minutes);
                IdentityModelEventSource.ShowPII = true;
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = CreatedDate,
                    Expires = ExpiredDate
                });
                var token = handler.WriteToken(securityToken);

                return new TokenModel()
                {
                    Authenticated = true,
                    Created = CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Expiration = ExpiredDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    AccessToken = token
                };

            }
            catch
            {
                return new TokenModel { Authenticated = false };
            }
        }

    }
}
