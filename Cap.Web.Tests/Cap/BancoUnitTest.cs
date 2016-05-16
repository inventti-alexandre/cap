using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Web.Tests.Cap
{
    [TestClass]
    public class BancoUnitTest
    {
        private IBaseService<Banco> service;

        public BancoUnitTest()
        {
            service = new BancoService();
        }

        [TestMethod]
        public void IncluirBanco()
        {
            // Arrange
            Banco banco = new Banco { AlteradoPor = 2, IdEmpresa = 2, Descricao = "ITAU", Razao = "ITAU UNIBANCO SA", NumFebraban = 341 };

            // Act
            banco.Id = service.Gravar(banco);

            // Assert
            Assert.IsTrue(banco.Id > 0);
        }
    }
}
