using Cap.Domain.Abstract.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class GraficoServiceTests
    {
        IGrafico service;

        public GraficoServiceTests()
        {
            service = new GraficoService();
        }

        [TestMethod()]
        public void GetGraficoTest()
        {
            // Arrange
            DateTime inicial = DateTime.Today.Date;
            DateTime final = DateTime.Today.Date.AddDays(10);
            int idEmpresa = 2;
            int idDepartamento = 0;
            int idPgto = 0;

            // Act
            var grafico = service.GetGrafico(inicial, final, idEmpresa, idDepartamento, idPgto);

            // Assert
            Assert.IsTrue(grafico.Count > 0);
        }
    }
}