using Cadastro.Domain.Base;
using Xunit;

namespace Cadastro.Tests.Extensions
{
    public static class AssertExtension
    {
        public static void CheckIfMessage(this DomainException exception, string message)
        {
            if (exception.Errors.Contains(message))
                Assert.True(true);
            else
                Assert.False(true, $"Esperava a mensagem '{message}'");
        }
    }
}
