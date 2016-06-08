using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;

namespace Cap.Domain.Service.Gen.Tests
{
    [TestClass()]
    public class IndiceServiceTests
    {
        private IBaseService<Indice> service;

        public IndiceServiceTests()
        {
            service = new IndiceService();
        }

        [TestMethod()]
        public void IndiceGravarTest()
        {
            // Arrange
            Indice item = new Indice { AlteradoPor = 2, Descricao = "IGPM", Nome = "INDICE GERAL DE PRECOS DE MERCADO" };

            // Act
            item.Id = service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}