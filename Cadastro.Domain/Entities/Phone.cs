using Cadastro.Domain.Base;
using System;

namespace Cadastro.Domain.Entities
{
    public class Phone
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Ddd { get; private set; }
        public string Number { get; private set; }

        private Phone()
        {

        }

        public Phone(string ddd, string number)
        {
            DomainValidate.Init()
                .When(string.IsNullOrEmpty(number), "O numero do telefone não pode ser em branco")
                .When(!string.IsNullOrEmpty(number) && string.IsNullOrEmpty(ddd), "O ddd não pode ser em branco")
                .ThrowExceptionIfExist();

            Ddd = ddd;
            Number = number;
        }

    }
}
