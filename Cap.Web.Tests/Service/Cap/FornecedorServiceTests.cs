using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class FornecedorServiceTests
    {
        private IBaseService<Fornecedor> service;

        public FornecedorServiceTests()
        {
            service = new FornecedorService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            var item = new Fornecedor { AlteradoPor = 2, Fantasia = "VIVO", Concessionaria = true, IdAgenda = 2, IdEmpresa = 2, IdPgto = 1 };

            // Act
            item.Id = service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}