using System;
using System.Collections.Generic;

namespace Cadastro.Domain
{
    public class DomainException : ArgumentException
    {
        public DomainException(string message) : base(message) { }

        public static void Quando(bool invalid, string message)
        {
            if (invalid)
                throw new DomainException(message);
        }
    }
}
