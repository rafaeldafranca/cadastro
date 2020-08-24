using System;
using System.Collections.Generic;

namespace Cadastro.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime Last_login { get; set; }

        public virtual List<Phone> Phones { get; set; }
    }
}