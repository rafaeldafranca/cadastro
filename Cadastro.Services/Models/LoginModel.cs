
using Cadastro.Services.Models;
using System;
using System.Collections.Generic;

namespace Cadastro.Domain.Models
{
    public class LoginModel : UserModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime Last_login { get; set; }
        public string Token { get; set; }

        public LoginModel(Guid id, string name, string email, DateTime created,
            DateTime? modified, DateTime last_login, List<PhoneUserModel> phones)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = null;
            Created = created;
            Modified = modified;
            Last_login = last_login;
            Phones = phones;
        }
    }
}