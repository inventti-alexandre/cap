using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;

namespace Cap.Domain.Service.Admin.Tests
{
    [TestClass()]
    public class SistemaParametroServiceTests
    {
        private IBaseService<SistemaParametro> service;

        public SistemaParametroServiceTests()
        {
            service = new SistemaParametroService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            var item = new SistemaParametro
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = 2,
                Ativo = true,
                Codigo = "LINK_COTACAO",
                Descricao = "URL PARA COTACAO",
                IdEmpresa = 1,
                Valor = "http://www.construtoraatlantica.com.br/Public/CotacaoFornecedor"
            };

            // Act
            service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}