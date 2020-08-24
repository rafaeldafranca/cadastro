using Cadastro.Core.Contexts;
using Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadastro.Tests
{
    public class MockContext
    {
        public MockContext()
        {
            InitContext();
        }

        protected PrincipalContext _context;

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<PrincipalContext>().UseInMemoryDatabase("CadastroUnitTest");

            var context = new PrincipalContext(builder.Options);
            var dtCadastro = DateTime.Now;

            if (context.User.Any())
            {
                _context = context;
                return;

            };

            var Users = Enumerable.Range(1, 10)
                .Select(i => new User
                {
                    Id = Guid.NewGuid(),
                    Created = dtCadastro,
                    Email = $"email{i}.teste.com",
                    Last_login = dtCadastro,
                    Modified = null,
                    Name = $"Name{1} Test",
                    Password = "123456",
                    Phones = new List<Phone>()
                    {
                        new  Phone { Ddd = "21", Number = new Random().Next().ToString() },
                        new  Phone { Ddd = "21", Number = new Random().Next().ToString() }
                    }
                });

            context.AddRange(Users);
            context.SaveChanges();

            _context = context;
        }
    }
}
