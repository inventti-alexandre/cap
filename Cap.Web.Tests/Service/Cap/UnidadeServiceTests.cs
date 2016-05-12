using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Service.Requisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class UnidadeServiceTests
    {
        private ILogin<Unidade> service;

        public UnidadeServiceTests()
        {
            service = new UnidadeService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            Unidade unidade = new Unidade { AlteradoPor = 1, Descricao = "M2" };

            // Act
            unidade.Id = service.Gravar(unidade);

            // Assert
            Assert.IsTrue(unidade.Id > 0);
        }
    }
}