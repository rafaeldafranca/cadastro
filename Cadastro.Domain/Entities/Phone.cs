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
            Ddd = ddd;
            Number = number;
        }

    }
}
