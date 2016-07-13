using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;

namespace Cap.Domain.Service.Requisicao.Tests
{
    [TestClass()]
    public class ReqAutorizanteServiceTests
    {
        private IBaseService<ReqAutorizante> service;

        public ReqAutorizanteServiceTests()
        {
            service = new ReqAutorizanteService();
        }

        [TestMethod()]
        public void ListarTest()
        {
            // Arrange
            List<ReqAutorizante> lista;

            // Act
            lista = service.Listar().ToList();

            // Assert
            Assert.IsTrue(lista != null);
        }
    }
}