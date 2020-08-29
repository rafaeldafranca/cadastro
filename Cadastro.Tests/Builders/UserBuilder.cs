using Bogus;
using Cadastro.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Cadastro.Tests.Builders
{
    public class UserBuilder
    {
        protected Guid Id;
        protected string Name;
        protected string Email;
        protected string Password;
        protected DateTime Created;
        protected DateTime? Modified;
        protected DateTime Last_login;
        protected dynamic Phone;

        public static UserBuilder Init()
        {
            var fake = new Faker();
            return new UserBuilder
            {
                Id = Guid.NewGuid(),
                Name = fake.Person.FullName,
                Email = fake.Person.Email,
                Password = fake.Random.AlphaNumeric(5),
                Created = DateTime.Now,
                Phone = new List<Phone>()
                {
                    new Phone(fake.Random.Int(2, 2).ToString(),
                              fake.Random.Int(9, 9).ToString()) ,
                     new Phone(fake.Random.Int(2, 2).ToString(),
                              fake.Random.Int(9, 9).ToString())
                }
            };
        }

        public UserBuilder WithName(string value)
        {
            Name = value;
            return this;
        }

        public UserBuilder WithEmail(string value)
        {
            Email = value;
            return this;
        }

        public UserBuilder WithPassword(string value)
        {
            Password = value;
            return this;
        }

        public UserBuilder WithId(Guid value)
        {
            Id = value;
            return this;
        }

        public User Build()
        {
            var fake = new User(Guid.Empty, Name, Email, Password, Phone);

            if (Id != Guid.Empty) return fake;

            var propertyInfo = fake.GetType().GetProperty("Id");
            propertyInfo.SetValue(fake, Convert.ChangeType(Id, propertyInfo.PropertyType), null);

            return fake;
        }
    }
}
