using Cadastro.Core.Contexts;
using Cadastro.Core.Repo;
using Cadastro.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadastro.Tests
{
    [TestClass]
    public class UserRepoUnitTest : MockContext
    {
        [TestMethod]
        public void TesteVerificaInstanciaContexto()
        {
            Assert.IsInstanceOfType(_context, typeof(PrincipalContext), "N�o foi instanciado");
        }

        [TestMethod]
        public void TesteAdicionarUsuario()
        {
            var UserMock = new User(
                Guid.Empty,
                "temp@test.com",
                "Ciclano da Silva",
                "123456",
                new List<Phone>()
                {
                    new Phone("21","333333"),
                    new Phone( "21","999999999")
                });

            var repo = new UserRepo(_context);
            var result = repo.Add(UserMock);

            Assert.IsNotNull(result, "O usu�rio deve voltar quando � criado");
            Assert.IsNotNull(result.Created, "A data de cria��o n�o pode ser nula");
            Assert.AreEqual(result.Created, result.Last_login, "Cria��o da data de ultimo login igual a data de cria��o");
            Assert.AreEqual(result.Id, result.Phones.ToList().FirstOrDefault().UserId, "A cria��o dos telefones devem ter a PK do usu�rio");

        }

        [TestMethod]
        public void TesteBuscarUsuarioPorId()
        {
            var repo = new UserRepo(_context);
            var user = _context.User.FirstOrDefault();

            var result = repo.Get(user.Id);

            Assert.IsNotNull(result, "O usu�rio existe e n�o foi carregado");
            Assert.IsNotNull(result.Phones, "A consulta retornou os telefones do usu�rio");

            result = repo.Get(Guid.NewGuid());
            Assert.IsNull(result, "O usu�rio n�o pode retornar quando ele n�o existir");
        }

        [TestMethod]
        public void TesteRecuperarTodosOsRegistros()
        {
            var repo = new UserRepo(_context);
            var result = repo.GetAll();

            Assert.IsNotNull(result, "N�o retornou resultados");
            Assert.IsTrue(result.Count() > 1, "N�o recuperou uma lista populada");

        }

        [TestMethod]
        public void TesteAlterarDataDeUltimoLogin()
        {
            var repo = new UserRepo(_context);
            var user = _context.User.FirstOrDefault();

            Assert.AreEqual(user.Last_login, user.Created, "As datas deveriam ser iguais para o teste");

            repo.LastLogin(user.Id);
            Assert.AreNotEqual(user.Last_login, user.Created, "As datas n�o devem permanecer iguais ap�s o ultimo login");
        }

    }
}
