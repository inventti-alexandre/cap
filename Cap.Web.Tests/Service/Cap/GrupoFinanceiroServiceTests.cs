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
    public class GrupoFinanceiroServiceTests
    {
        private IBaseService<GrupoFinanceiro> service;

        public GrupoFinanceiroServiceTests()
        {
            service = new GrupoFinanceiroService();
        }

        [TestMethod()]
        public void GrupoFinanceiroGravarTest()
        {
            // Arrange
            var item = new GrupoFinanceiro { AlteradoPor = 2, IdEmpresa = 2, Descricao = "DESPESAS ADMINISTRATIVAS" };

            // Act
            item.Id = service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}