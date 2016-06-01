using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;

namespace Cap.Domain.Service.Admin.Tests
{
    [TestClass()]
    public class SistemaAreaServiceTests
    {
        private IBaseService<SistemaArea> service;

        public SistemaAreaServiceTests()
        {
            service = new SistemaAreaService();
        }

        [TestMethod()]
        public void SistemaAreaGravarTest()
        {
            // Arrange
            SistemaArea area = new SistemaArea { AlteradoPor = 2, Descricao = "ERP" };

            // Act
            area.Id = service.Gravar(area);

            // Assert
            Assert.IsTrue(area.Id > 0);
        }
    }
}