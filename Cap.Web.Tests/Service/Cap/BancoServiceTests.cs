using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class BancoServiceTests
    {
        IBaseService<Banco> service;

        public BancoServiceTests()
        {
            service = new BancoService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            var item = new Banco { AlteradoPor = 2, Descricao = "ITAU", IdEmpresa = 2, NumFebraban = 341, Razao = "BANCO ITAU UNIBANCO SA" };

            // Act
            item.Id = service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}