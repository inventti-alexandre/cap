using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Web.Tests.Cap
{
    [TestClass]
    public class FPgtoUnitTest
    {
        private IBaseService<FPgto> service;

        public FPgtoUnitTest()
        {
            service = new FPgtoService();
        }

        [TestMethod]
        public void IncluirFPgtos()
        {
            // Arrange
            var fpgto01 = new FPgto { AlteradoPor = 2, IdEmpresa = 2, Ativo = true, Descricao = "BANCO" };
            var fpgto02 = new FPgto { AlteradoPor = 2, IdEmpresa = 2, Ativo = true, Descricao = "CHEQUE" };
            var fpgto03 = new FPgto { AlteradoPor = 2, IdEmpresa = 2, Ativo = true, Descricao = "DEPOSITO" };
            var fpgto04 = new FPgto { AlteradoPor = 2, IdEmpresa = 2, Ativo = true, Descricao = "DINHEIRO" };
            var fpgto05 = new FPgto { AlteradoPor = 2, IdEmpresa = 2, Ativo = true, Descricao = "RETENCAO" };

            // Act
            fpgto01.Id = service.Gravar(fpgto01);
            fpgto02.Id = service.Gravar(fpgto02);
            fpgto03.Id = service.Gravar(fpgto03);
            fpgto04.Id = service.Gravar(fpgto04);
            fpgto05.Id = service.Gravar(fpgto05);

            // Assert
            Assert.IsTrue(fpgto01.Id > 0);
            Assert.IsTrue(fpgto02.Id > 0);
            Assert.IsTrue(fpgto03.Id > 0);
            Assert.IsTrue(fpgto04.Id > 0);
            Assert.IsTrue(fpgto05.Id > 0);
        }
    }
}
