using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Web.Tests.Cap
{
    [TestClass]
    public class PgtoUnitTest
    {
        private ILogin<Pgto> service;

        public PgtoUnitTest()
        {
            service = new PgtoService();
        }

        [TestMethod]
        public void IncluirPgtos()
        {
            // Arrange
            var pgto01 = new Pgto { AlteradoPor = 1, Ativo = true, Descricao = "BANCO", Imposto = false };
            var pgto02 = new Pgto { AlteradoPor = 1, Ativo = true, Descricao = "RETIRA", Imposto = false };
            var pgto03 = new Pgto { AlteradoPor = 1, Ativo = true, Descricao = "DEPOSITO", Imposto = false };
            var pgto04 = new Pgto { AlteradoPor = 1, Ativo = true, Descricao = "DINHEIRO", Imposto = false };
            var pgto05 = new Pgto { AlteradoPor = 1, Ativo = true, Descricao = "DEBITO AUT.", Imposto = false };

            // Act
            pgto01.Id = service.Gravar(pgto01);
            pgto02.Id = service.Gravar(pgto02);
            pgto03.Id = service.Gravar(pgto03);
            pgto04.Id = service.Gravar(pgto04);
            pgto05.Id = service.Gravar(pgto05);

            // Assert
            Assert.IsTrue(pgto01.Id > 0);
            Assert.IsTrue(pgto02.Id > 0);
            Assert.IsTrue(pgto03.Id > 0);
            Assert.IsTrue(pgto04.Id > 0);
            Assert.IsTrue(pgto05.Id > 0);
        }
    }
}
