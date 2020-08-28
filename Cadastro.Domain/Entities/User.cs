using System;
using System.Collections.Generic;

namespace Cadastro.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Modified { get; private set; }
        public DateTime Last_login { get; private set; }

        public virtual List<Phone> Phones { get; set; }

        private User()
        {
        }

        public User(Guid id, string name, string email, string password, List<Phone> phones = null)
        {
            if (id == Guid.Empty)
                Id = Guid.NewGuid();

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("O nome não pode ser em branco");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("O email não pode ser em branco");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("O senha não pode ser em branco");

            Name = name;
            Email = email;
            Password = password;
            Phones = phones;
            Created = DateTime.Now;
            Last_login = Created;
        }

        public void RegistrarAcesso()
        {
            Last_login = DateTime.Now;
        }
    }
}