using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cap.Web.Tests.Admin
{
    [TestClass]
    public class FeriadoUnitTest
    {
        private IBaseService<Feriado> service;

        public FeriadoUnitTest()
        {
            service = new FeriadoService();
        }

        [TestMethod]
        public void IncluirFeriado()
        {
            // Arrange
            Feriado feriado = new Feriado { AlteradoPor = 2, IdEmpresa = 2, Data = new DateTime(2015, 1, 1), Descricao = "CONFRATERNIZAÇÃO UNIVERSAO" };

            // Act
            feriado.Id = service.Gravar(feriado);

            // Assert
            Assert.IsTrue(feriado.Id > 0);
        }
    }
}
