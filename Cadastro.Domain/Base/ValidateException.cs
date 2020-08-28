using System.Collections.Generic;
using System.Linq;

namespace Cadastro.Domain.Base
{
    public class ValidateException
    {
        private readonly List<string> _errors;
        private ValidateException()
        {
            _errors = new List<string>();
        }

        public static ValidateException New()
        {
            return new ValidateException();
        }

        public ValidateException When(bool exists, string msg)
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
}
