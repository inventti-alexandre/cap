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
    public class DepositoServiceTests
    {
        IBaseService<Deposito> service;

        public DepositoServiceTests()
        {
            service = new DepositoService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            var item = new Deposito { Agencia = "6241", AlteradoPor = 2, Conta = "00356-0", Favorecido = "JOSE ALESSANDRO", IdBanco = 1, IdEmpresa = 2 };

            // Act
            item.Id = service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}