using Cadastro.Domain.Entities;
using Cadastro.Domain.Models;
using Cadastro.Domain.VO;
using System.Collections.Generic;

namespace Cadastro.Domain.Adapters
{
    public static class UserAdapter
    {
        public static User Adapter(this UserModel data)
        {
            if (data == null) return null;

            User user = new User()
            {
                Email = data.Email,
                Name = data.Name,
                Password = data.Password,
            };

            if (data.Phones != null)
            {
                user.Phones = new List<Phone>();
                foreach (var item in data.Phones)
                    user.Phones.Add(item.Adapter());
            }

            return user;
        }

        public static LoginModel Adapter(this User data)
        {
            if (data == null) return null;

            LoginModel user = new LoginModel()
            {
                Id = data.Id,
                Email = data.Email,
                Name = data.Name,
                Created = data.Created,
                Last_login = data.Last_login,
               // Password = data.Password,
                Modified = data.Modified
            };

            if (data.Phones != null)
            {
                user.Phones = new List<PhoneUserModel>();
                foreach (var item in data.Phones)
                    user.Phones.Add(new PhoneUserModel() { Ddd = item.Ddd, Number = item.Number });
            }

            return user;
        }

    }
}
