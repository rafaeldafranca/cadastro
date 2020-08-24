using Cadastro.Domain.Entities;
using Cadastro.Domain.Interfaces.Base;

namespace Cadastro.Domain.Interfaces.Repos
{
    public interface IUserRepo : IRepoBase<User>
    {
        bool CheckUser(string email);
        User Validate(string email, string password);
    }
}
