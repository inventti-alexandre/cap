using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Models.Cap;
using Cap.Domain.Abstract;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class GrupoCustoServiceTests
    {
        private IBaseService<GrupoCusto> service;

        public GrupoCustoServiceTests()
        {
            service = new GrupoCustoService();
        }

        [TestMethod()]
        public void GrupoCustoGravarTest()
        {
            // Arrange
            GrupoCusto item = new GrupoCusto
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = 2,
                Descricao = "PAPELARIA",
                IdEmpresa = 2
            };

            // Act
            item.Id = service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}