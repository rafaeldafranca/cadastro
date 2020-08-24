using Cadastro.Domain.VO;
using System;
using System.Collections.Generic;

namespace Cadastro.Domain.Models
{
    public class LoginModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime Last_login { get; set; }
        public string Token { get; set; }
        public virtual List<PhoneUserModel> Phones { get; set; }
    }
}
