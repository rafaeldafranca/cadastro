using Cadastro.Domain.Entities;
using Cadastro.Domain.Models;
using Cadastro.Services.Interfaces.Base;
using Cadastro.Services.Models;

namespace Cadastro.Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase<User>
    {
        bool CheckUser(string email);
        LoginModel Validate(string email, string password);
    }

}
