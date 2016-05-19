using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class ContaTipoServiceTests
    {
        private IBaseService<Models.Cap.ContaTipo> service;

        public ContaTipoServiceTests()
        {
            service = new ContaTipoService();
        }

        [TestMethod()]
        public void ContaTipoGravarTest()
        {
            // Arrange
            Models.Cap.ContaTipo tipo = new Models.Cap.ContaTipo { AlteradoPor = 2, Descricao = "POUPANCA", IdEmpresa = 2 };

            // Act
            tipo.Id = service.Gravar(tipo);

            // Assert
            Assert.IsTrue(tipo.Id > 0);
        }
    }
}