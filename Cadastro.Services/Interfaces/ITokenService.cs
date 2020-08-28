using Cadastro.Domain.Entities;
using Cadastro.Services.Configs;
using Cadastro.Services.Models;

namespace Cadastro.Services.Interfaces
{
    public interface ITokenService
    {
        TokenModel Token(User user, SigningConfig signingConfigurations
                                  , TokenConfig tokenConfigurations);
    }
}
