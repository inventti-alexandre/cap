using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Abstract;

namespace Cap.Domain.Service.Requisicao.Tests
{
    [TestClass()]
    public class CotFornecedorServiceTests
    {
        private IBaseService<CotFornecedor> service;

        public CotFornecedorServiceTests()
        {
            service = new CotFornecedorService();
        }

        [TestMethod()]
        public void CotFornecedorExcluirTest()
        {
            // Arrange
            var item = new CotFornecedor { CotGrupoId = 1, Email = "jb.alessandro@gmail.com", FornecedorId = 1, UsuarioId = 2 };

            // Act
            service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}