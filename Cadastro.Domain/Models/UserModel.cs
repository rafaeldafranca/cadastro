using System.Collections.Generic;

namespace Cadastro.Domain.VO
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual List<PhoneUserModel> Phones { get; set; }

    }

}
