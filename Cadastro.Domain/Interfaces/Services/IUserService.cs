using Cadastro.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Cadastro.Domain.Interfaces.Services
{
    public interface IUserService
    {
        bool CheckUser(string email);
        User Validate(string email, string password);
        User Add(User user);
        IEnumerable<User> GetAll();
        User GetbyId(Guid Id);
    }
}
