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
    public class ParcelaServiceTests
    {
        private IBaseService<Parcela> service;

        public ParcelaServiceTests()
        {
            service = new ParcelaService();
        }

        [TestMethod()]
        public void ParcelaGravarTest()
        {
            // Arrange
            var parcela = new Parcela
            {
                AlteradoPor = 2,
                CriadoEm = DateTime.Now,
                CriadoPor = 2,
                IdEmpresa = 2,
                IdPgto = 2,
                IdMoeda = 1,
                IdPedido = 2,
                Valor = 10,
                Vencto = new DateTime(2016, 05, 25)
            };

            // Act
            parcela.Id = service.Gravar(parcela);

            // Assert
            Assert.IsTrue(parcela.Id > 0);
        }
    }
}