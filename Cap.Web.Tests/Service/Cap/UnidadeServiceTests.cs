using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Service.Requisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class UnidadeServiceTests
    {
        private IBaseService<Unidade> service;

        public UnidadeServiceTests()
        {
            service = new UnidadeService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            Unidade unidade = new Unidade { AlteradoPor = 2, Descricao = "M2", IdEmpresa = 2 };

            // Act
            unidade.Id = service.Gravar(unidade);

            // Assert
            Assert.IsTrue(unidade.Id > 0);
        }
    }
}