using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class LiberacaoServiceTests
    {
        [TestMethod()]
        public void ParcelasALiberarTest()
        {
            // Arrange
            int idUsuario = 2;
            var liberacao = new LiberacaoService();

            // Act
            var parcelas = liberacao.ParcelasALiberar(idUsuario, DateTime.Today.Date.AddMonths(2));

            // Assert
            Assert.IsTrue(parcelas.Count > 0);
        }
    }
}