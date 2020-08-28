using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadastro.Domain.Base
{
    public class DomainValidate
    {
        private readonly List<string> _errors;

        private DomainValidate() => _errors = new List<string>();

        public static DomainValidate Init() => new DomainValidate();

        public DomainValidate When(bool exists, string msg)
        {
            if (exists)
                _errors.Add(msg);

            return this;
        }

        public void ThrowExceptionIfExist()
        {
            if (_errors.Any())
                throw new DomainException(_errors);
        }
    }

    public class DomainException : ArgumentException
    {
        public List<string> Errors { get; set; }
        public DomainException(List<string> errors) => Errors = errors;
    }
}
