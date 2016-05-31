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
    public class ContaFinanceiraServiceTests
    {
        private IBaseService<ContaFinanceira> service;

        public ContaFinanceiraServiceTests()
        {
            service = new ContaFinanceiraService();
        }

        [TestMethod()]
        public void ContaFinanceiraGravarTest()
        {
            // Arrange
            var item = new ContaFinanceira { AlteradoPor = 2, Descricao = "PAPELARIA", IdEmpresa = 2, IdGrupoFinanceiro = 1 };

            // Act
            item.Id = service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}