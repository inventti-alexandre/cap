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
    public class PedidoServiceTests
    {
        private IBaseService<Pedido> service;

        public PedidoServiceTests()
        {
            service = new PedidoService();
        }

        [TestMethod()]
        public void PedidoGravarTest()
        {
            // Arrange
            var pedido = new Pedido
            {
                AlteradoPor = 2,
                CriadoPor = 2,
                IdDepartamento = 1,
                IdEmpresa = 1,
                IdFornecedor = 2
            };

            // Act
            pedido.Id = service.Gravar(pedido);

            // Assert
            Assert.IsTrue(pedido.Id > 0);
        }
    }
}