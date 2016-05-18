using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class MoedaServiceTests
    {
        private IBaseService<Moeda> service;

        public MoedaServiceTests()
        {
            service = new MoedaService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            Moeda moeda = new Moeda { AlteradoPor = 2, IdEmpresa = 2, Descricao = "R$", Padrao = true };

            // Act
            moeda.Id = service.Gravar(moeda);

            // Assert
            Assert.IsTrue(moeda.Id > 0);
        }
    }
}