using Cadastro.Domain.Entities;
using Cadastro.Domain.Interfaces.Repos;
using Cadastro.Domain.Interfaces.Services;
using Cadastro.Domain.Models;
using Cadastro.Services.Models;
using Cadastro.Util;
using System;
using System.Collections.Generic;

namespace Cadastro.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public User Add(User user) => _userRepo.Add(user);

        public bool CheckUser(string email) => _userRepo.CheckUser(email);

        public LoginModel Validate(string email, string password)
        {
            var pwd = password.GetCadastroHashCode();
            var data = _userRepo.Validate(email, pwd);
            if (data == null) return null;

            List<PhoneUserModel> phones = null;
            if (data.Phones != null)
            {
                phones = new List<PhoneUserModel>();
                data.Phones.ForEach(q =>
                   {
                       phones.Add(new PhoneUserModel() { Ddd = q.Ddd, Number = q.Number });
                   });
            }

            return new LoginModel(data.Id, data.Name, data.Email, data.Created
                                , data.Modified, data.Last_login, phones);

        }

        public User GetbyId(Guid Id) => _userRepo.Get(Id);

        public IEnumerable<User> GetAll() => _userRepo.GetAll();

    }
}