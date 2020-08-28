using Bogus;
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

            var fake = new Faker();

            var Users = Enumerable.Range(1, 10)
                .Select(i => new User(
                    Guid.NewGuid(),
                     fake.Person.Email,
                     fake.Person.FullName,
                    "123456",
                     new List<Phone>()                    
                     {
                        new  Phone ("21", fake.Person.Phone ),
                        new  Phone ("21", fake.Person.Phone )
                    }
                ));

            context.AddRange(Users);
            context.SaveChanges();

            _context = context;
        }
    }
}
