using System;

namespace Cadastro.Domain.Entities
{
    public class Phone
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Ddd { get; set; }
        public string Number { get; set; }
    }
}
