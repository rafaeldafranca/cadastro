using System;
using System.Collections.Generic;

namespace Cadastro.Domain
{
    public class DomainException : ArgumentException
    {
        public List<string> Errors { get; set; }

        public DomainException(List<string> errors)
        {
            Errors = errors;
        }
    }
}
