using Cadastro.Core.Contexts;
using Cadastro.Domain.Entities;
using Cadastro.Domain.Interfaces.Repos;
using Dapper;
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
            //dapper comentado devido ao banco de memória.
            //var cn = _context.Database.GetDbConnection();

            //string sql = @"
            //    SELECT u.Id, u.Name, u.Email, u.Password, u.Created, u.Modified, u.Last_login,
            //    p.Id, p.UserId, p.Ddd, p.Number 
            //    FROM Users u
            //    INNER JOIN Phones p
            //    on u.Id = p.UserId
            //    order by u.Id";

            //var dapperDictionary = new Dictionary<Guid, User>();

            //var resultDapper = cn.Query<User, Phone, User>(sql,
            //     (user, phone) =>
            //     {
            //         if (!dapperDictionary.TryGetValue(user.Id, out User userResult))
            //         {
            //             userResult = user;
            //             if (userResult.Phones == null && phone != null)
            //                 userResult.Phones = new List<Phone>();
            //             dapperDictionary.Add(user.Id, userResult);
            //         }
            //         if (phone != null)
            //             userResult.Phones.Add(phone);

            //         return userResult;
            //     },
            //     splitOn: "Id")
            //    .Distinct()
            //    .ToList();

            //return resultDapper;

            var result = _context.User
                    .Include(x => x.Phones)
                    .ToList();

            return result;
        }

        public User Add(User entity)
        {
            entity.Created = DateTime.Now;
            entity.Last_login = entity.Created;

            _context.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public User Get(Guid id)
        {
            /*---------------------------------------------------------
             * dapper comentado devido ao banco de memória.
             *---------------------------------------------------------*/
             
            //var cn = _context.Database.GetDbConnection();

            //string sql = @"
            //    SELECT u.Id, u.Name, u.Email, u.Password, u.Created, u.Modified, u.Last_login,
            //    p.Id, p.UserId, p.Ddd, p.Number 
            //    FROM Users u
            //    INNER JOIN Phones p
            //    on u.Id = p.UserId
            //    WHERE u.Id = @id
            //    order by u.Id";

            //var dapperDictionary = new Dictionary<Guid, User>();

            //var resultDapper = cn.Query<User, Phone, User>(sql,
            //     (user, phone) =>
            //     {
            //         if (!dapperDictionary.TryGetValue(user.Id, out User userResult))
            //         {
            //             userResult = user;
            //             if (userResult.Phones == null && phone != null)
            //                 userResult.Phones = new List<Phone>();
            //             dapperDictionary.Add(user.Id, userResult);
            //         }
            //         if (phone != null)
            //             userResult.Phones.Add(phone);

            //         return userResult;
            //     },
            //      param: new { id },
            //     splitOn: "Id")
            //    .Distinct()
            //    .SingleOrDefault();

            //return resultDapper;

            //usando o EF no inmemory
            var result = _context.User.AsQueryable()
               .Include(x => x.Phones)
               .Where(x => x.Id == id)
               .SingleOrDefault();

            return result;
        }

        public void LastLogin(Guid UserId)
        {
            var user = Get(UserId);
            user.Last_login = DateTime.Now;
            _context.User.Update(user);
            _context.SaveChanges();
        }

        public User Validate(string email, string password)
        {
            /*---------------------------------------------------------
             * dapper comentado devido ao banco de memória.
             *---------------------------------------------------------*/
            //var cn = _context.Database.GetDbConnection();

            //string sql = @"
            //    SELECT u.Id, u.Name, u.Email, u.Password, u.Created, u.Modified, u.Last_login,
            //    p.Id, p.UserId, p.Ddd, p.Number 
            //    FROM Users u
            //    INNER JOIN Phones p
            //    on u.Id = p.UserId
            //    WHERE u.Email = @email and u.Password = @password
            //    order by u.Id";

            //var dapperDictionary = new Dictionary<Guid, User>();

            //var resultDapper = cn.Query<User, Phone, User>(sql,
            //     (user, phone) =>
            //     {
            //         if (!dapperDictionary.TryGetValue(user.Id, out User userResult))
            //         {
            //             userResult = user;
            //             if (userResult.Phones == null && phone != null)
            //                 userResult.Phones = new List<Phone>();
            //             dapperDictionary.Add(user.Id, userResult);
            //         }
            //         if (phone != null)
            //             userResult.Phones.Add(phone);

            //         return userResult;
            //     },
            //      param: new { email, password },
            //     splitOn: "Id")
            //    .Distinct()
            //    .SingleOrDefault();

            //return resultDapper;

            //usando o EF no inmemory
            var result = _context.User.AsQueryable()
              .Include(x => x.Phones)
              .Where(q => q.Email == email && q.Password == password)
              .SingleOrDefault();

            if (result != null) //usuario logado !
            {
                result.Last_login = DateTime.Now;
                _context.User.Update(result);
                _context.SaveChanges();
            }

            return result;
        }

        public bool CheckUser(string email)
        {
            /*---------------------------------------------------------
             * dapper comentado devido ao banco de memória.
             *---------------------------------------------------------*/
            //var cn = _context.Database.GetDbConnection();

            //string sql = "SELECT * FROM Users u WHERE u.Email = @email";
            //var resultDapper = cn.QueryFirstOrDefault<User>(sql, new { Email = email });
            //return (resultDapper != null);

            //usando o EF no inmemory
            var query = from q in _context.User.AsQueryable()
                        where q.Email == email
                        select q;

            return query.Any();
        }

    }
}
