using Cadastro.Domain.Models;
using Cadastro.Services.Configs;
using Cadastro.Services.Models;

namespace Cadastro.Services.Interfaces
{
    public interface ITokenService
    {
        TokenModel Token(LoginModel user, SigningConfig signingConfigurations
                                  , TokenConfig tokenConfigurations);
    }
}
