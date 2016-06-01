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
    public class SistemaRegraServiceTests
    {
        IBaseService<SistemaRegra> service;

        public SistemaRegraServiceTests()
        {
            service = new SistemaRegraService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            SistemaRegra regra = new SistemaRegra { AlteradoPor = 2, Descricao = "admin", Observ = "Administrador do sistema" };

            // Act
            regra.Id = service.Gravar(regra);

            // Assert
            Assert.IsTrue(regra.Id > 0);
        }
    }
}