﻿using Cadastro.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        private readonly Regex _emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        private User()
        {
        }

        public User(Guid id, string name, string email, string password, List<Phone> phones = null)
        {
            if (id == Guid.Empty)
                Id = Guid.NewGuid();

            DomainValidate.Init()
                .When(string.IsNullOrEmpty(name), "O nome não pode ser em branco")
                .When(string.IsNullOrEmpty(email), "O email não pode ser em branco")
                .When(string.IsNullOrEmpty(password), "A senha não pode ser em branco")
                .When(string.IsNullOrEmpty(email) || !_emailRegex.Match(email).Success, "O email deve ser preenchido corretamente")
                .ThrowExceptionIfExist();

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