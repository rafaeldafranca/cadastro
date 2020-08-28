using Cadastro.Domain.Entities;
using Cadastro.Domain.Interfaces.Repos;
using Cadastro.Domain.Interfaces.Services;
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

        public User Add(User user)
        {
            //user.Password = user.Password.GetCadastroHashCode();
            return _userRepo.Add(user);
        }
        
        public IEnumerable<User> GetAll()
        {
            return _userRepo.GetAll();
        }

        public bool CheckUser(string email)
        {
            return _userRepo.CheckUser(email);
        }

        public User Validate(string email, string password)
        {
            var pwd = password.GetCadastroHashCode();
            return _userRepo.Validate(email, pwd);
        }

        public User GetbyId(Guid Id)
        {
            return _userRepo.Get(Id);
        }
    }
}