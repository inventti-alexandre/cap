using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Web.Tests.Admin
{
    [TestClass]
    public class EstadoCivilUnitTest
    {
        private IBaseService<EstadoCivil> service;

        public EstadoCivilUnitTest()
        {
            service = new EstadoCivilService();
        }

        [TestMethod]
        public void IncluirEstadosCivis()
        {
            // Arrange
            var civil01 = new EstadoCivil { AlteradoPor = 2, Ativo = true, Descricao = "SOLTEIRO" };
            var civil02 = new EstadoCivil { AlteradoPor = 2, Ativo = true, Descricao = "CASADO" };
            var civil03 = new EstadoCivil { AlteradoPor = 2, Ativo = true, Descricao = "SEPARADO JUDICIALMENTE" };
            var civil04 = new EstadoCivil { AlteradoPor = 2, Ativo = true, Descricao = "DIVORCIADO" };
            var civil05 = new EstadoCivil { AlteradoPor = 2, Ativo = true, Descricao = "VIUVO" };
            var civil06 = new EstadoCivil { AlteradoPor = 2, Ativo = true, Descricao = "OUTROS" };

            // Act
            civil01.Id = service.Gravar(civil01);
            civil02.Id = service.Gravar(civil02);
            civil03.Id = service.Gravar(civil03);
            civil04.Id = service.Gravar(civil04);
            civil05.Id = service.Gravar(civil05);
            civil06.Id = service.Gravar(civil06);

            // Assert
            Assert.IsTrue(civil01.Id > 0);
            Assert.IsTrue(civil02.Id > 0);
            Assert.IsTrue(civil03.Id > 0);
            Assert.IsTrue(civil04.Id > 0);
            Assert.IsTrue(civil05.Id > 0);
            Assert.IsTrue(civil06.Id > 0);
        }
    }
}
