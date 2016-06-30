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
    public class CotCotacaoServiceTests
    {
        private IBaseService<CotCotacao> service;

        public CotCotacaoServiceTests()
        {
            service = new CotCotacaoService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            var item1 = new CotCotacao { FornecedorId = 1, Observ = "", Preco = 10, ReqRequisicaoId = 1, ReqMaterialId = 1 };
            var item2 = new CotCotacao { FornecedorId = 1, Observ = "", Preco = 20, ReqRequisicaoId = 1, ReqMaterialId = 2 };
            var item3 = new CotCotacao { FornecedorId = 1, Observ = "", Preco = 30, ReqRequisicaoId = 1, ReqMaterialId = 3 };

            // Act
            service.Gravar(item1);
            service.Gravar(item2);
            service.Gravar(item3);

            // Assert
            Assert.IsTrue(item1.Id > 0);
            Assert.IsTrue(item2.Id > 0);
            Assert.IsTrue(item3.Id > 0);
        }
    }
}