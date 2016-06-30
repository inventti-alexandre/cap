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
    public class CotDadosCotacaoServiceTests
    {
        private IBaseService<CotDadosCotacao> service;

        public CotDadosCotacaoServiceTests()
        {
            service = new CotDadosCotacaoService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            var item = new CotDadosCotacao { AlteradoEm = DateTime.Now, Condicao = "30ddp", Contato = "JOSE", CotCotadoComId = 1, Frete = 0, Imposto = 0, Observ = "", PrevisaoEntrega = "2ddp", Validade = "5 dias" };

            // Act
            service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}