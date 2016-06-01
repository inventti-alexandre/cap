using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class RegimeTributarioServiceTests
    {
        private IBaseService<RegimeTributario> service;

        public RegimeTributarioServiceTests()
        {
            service = new RegimeTributarioService();
        }

        [TestMethod()]
        public void RegimeTributarioExcluirTest()
        {
            // Arrange
            var item1 = new RegimeTributario { AlteradoPor = 2, Descricao = "LUCRO REAL" };
            var item2 = new RegimeTributario { AlteradoPor = 2, Descricao = "LUCRO PRESUMIDO" };
            var item3 = new RegimeTributario { AlteradoPor = 2, Descricao = "SIMPLES NACIONAL" };

            // Act
            item1.Id = service.Gravar(item1);
            item2.Id = service.Gravar(item2);
            item3.Id = service.Gravar(item3);

            // Assert
            Assert.IsTrue(service.Listar().Count() > 2);
        }
    }
}