using Cadastro.Domain.Entities;
using Cadastro.Domain.Models;
using Cadastro.Services.Models;
using Cadastro.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadastro.Services.Adapters
{
    public static class UserModelAdapter
    {
        public static User Adapter(this UserModel data)
        {
            if (data == null) return null;
            List<Phone> phones = null;
            if (data.Phones != null)
            {
                phones = new List<Phone>();
                phones.AddRange(data.Phones.Select(q => new Phone(q.Ddd, q.Number)));
            }

            return new User(
                Guid.Empty,
                data.Name,
                data.Email,
                data.Password.GetCadastroHashCode(),
                phones
                );
        }

        public static UserModel Adapter(this User data)
        {
            if (data == null) return null;
            List<PhoneUserModel> phones = null;
            if (data.Phones != null)
            {
                phones = new List<PhoneUserModel>();
                phones.AddRange(
                    data.Phones.Select(q =>
                    new PhoneUserModel
                    {
                        Ddd = q.Ddd,
                        Number = q.Number
                    })
               );
            }

            UserModel user = new UserModel()
            {
                Email = data.Email,
                Name = data.Name,
                Phones = phones
            };

            return user;
        }
    }
}
