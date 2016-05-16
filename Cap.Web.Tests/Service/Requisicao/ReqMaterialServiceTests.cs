using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Requisicao.Tests
{
    [TestClass()]
    public class ReqMaterialServiceTests
    {
        private IBaseService<ReqMaterial> service;

        public ReqMaterialServiceTests()
        {
            service = new ReqMaterialService();
        }

        [TestMethod()]
        public void ReqMaterialGravarTest()
        {
            // Arrange
            ReqMaterial item = new ReqMaterial { AlteradoPor = 2, IdMaterial = 2, IdReqRequisicao = 2, Qtde = 2 };

            // Act
            item.Id = service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}