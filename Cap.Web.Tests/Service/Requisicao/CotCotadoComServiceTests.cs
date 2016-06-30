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
    public class CotCotadoComServiceTests
    {
        private IBaseService<CotCotadoCom> service;

        public CotCotadoComServiceTests()
        {
            service = new CotCotadoComService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            var item = new CotCotadoCom { FornecedorId = 1, ReqRequisicaoId = 1, UsuarioId = 2 };

            // Act
            service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}