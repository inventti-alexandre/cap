using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Service.Requisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class MaterialServiceTests
    {
        private ILogin<Material> service;

        public MaterialServiceTests()
        {
            service = new MaterialService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            Material material = new Material { AlteradoPor = 1, Descricao = "CABO 6MM", IdMatGrupo = 1, IdUnidade = 2, QtdeMinimaPedido = 10 };

            // Act
            material.Id = service.Gravar(material);

            // Assert
            Assert.IsTrue(material.Id > 0);
        }
    }
}