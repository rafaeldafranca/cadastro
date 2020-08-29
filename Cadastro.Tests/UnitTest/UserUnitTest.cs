using Cadastro.Domain.Base;
using Cadastro.Tests.Builders;
using System;
using Xunit;

namespace Cadastro.Tests.Extensions
{
    public class UserUnitTest
    {

        [Theory]
        [InlineData("70ce7378-bfe1-4564-9a71-db45552131f3")]
        [InlineData(null)]
        public void CriandoUsuario(string value)
        {
            Guid UserId = string.IsNullOrEmpty(value) ? Guid.Empty : Guid.Parse(value);
            var esperado = UserBuilder.Init().WithId(UserId).Build();
            Assert.NotNull(esperado);
        }

        [Fact]
        public void CriarComNameNulo()
        {
            Assert.Throws<DomainException>(() => UserBuilder.Init().WithName(null).Build())
                                          .CheckIfMessage("O nome não pode ser em branco");
        }

        [Fact]
        public void CriarComEmailNulo()
        {
            Assert.Throws<DomainException>(() => UserBuilder.Init().WithEmail(null).Build())
                                           .CheckIfMessage("O email não pode ser em branco");
        }

        [Fact]
        public void CriarComPasswordNulo()
        {
            Assert.Throws<DomainException>(() => UserBuilder.Init().WithPassword(null).Build())
                                           .CheckIfMessage("A senha não pode ser em branco");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("email@")]
        [InlineData("emaildominio.com")]
        public void CriarComEmailErrado(string value)
        {
            Assert.Throws<DomainException>(() => UserBuilder.Init().WithEmail(value).Build())
                                           .CheckIfMessage("O email deve ser preenchido corretamente");
        }
    }
}
