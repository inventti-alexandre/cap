using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Abstract;

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
            ReqMaterial item = new ReqMaterial { AlteradoPor = 1, IdMaterial = 2, IdReqRequisicao = 2, Qtde = 2 };

            // Act
            item.Id = service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}