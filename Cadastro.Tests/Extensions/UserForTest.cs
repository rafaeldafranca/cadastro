using Cadastro.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Cadastro.Tests.Extensions
{
    public class UserForTest : User
    {
        public UserForTest(Guid id, string name, string email, string password, List<Phone> phones = null) 
            : base(id, name, email, password, phones)
        {
        }

    }
}
