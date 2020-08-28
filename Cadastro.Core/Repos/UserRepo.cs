using Cadastro.Core.Contexts;
using Cadastro.Domain.Entities;
using Cadastro.Domain.Interfaces.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadastro.Core.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly PrincipalContext _context;

        public UserRepo(PrincipalContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAll()
        {
           
            var result = _context.User
                    .Include(x => x.Phones)
                    .ToList();

            return result;
        }

        public User Add(User entity)
        {
            _context.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public User Get(Guid id)
        {
            var result = _context.User.AsQueryable()
               .Include(x => x.Phones)
               .Where(x => x.Id == id)
               .SingleOrDefault();

            return result;
        }

        public void LastLogin(Guid UserId)
        {
            var user = Get(UserId);
            user.RegistrarAcesso();
            _context.User.Update(user);
            _context.SaveChanges();
        }

        public User Validate(string email, string password)
        {
           
            var result = _context.User.AsQueryable()
              .Include(x => x.Phones)
              .Where(q => q.Email == email && q.Password == password)
              .SingleOrDefault();

            if (result != null) //usuario logado !
            {
                result.RegistrarAcesso();
                _context.User.Update(result);
                _context.SaveChanges();
            }

            return result;
        }

        public bool CheckUser(string email)
        {
            var query = from q in _context.User.AsQueryable()
                        where q.Email == email
                        select q;

            return query.Any();
        }

    }
}
