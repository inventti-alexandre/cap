using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Abstract.Req;

namespace Cap.Domain.Service.Requisicao.Tests
{
    [TestClass()]
    public class CotCotadoComServiceTests
    {
        private IBaseService<CotCotadoCom> service;
        private ICotadoCom serviceCotCom;

        public CotCotadoComServiceTests()
        {
            service = new CotCotadoComService();
            serviceCotCom = new CotCotadoComService();
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

        [TestMethod()]
        public void GetCotacaoFornecedorTest()
        {
            // Arrange
            CotCotadoCom item;
            int idRequisicao = 1;
            int idFornecedor = 1;
            List<int> fornecedores = new List<int>();
            fornecedores.Add(idFornecedor);
            int idUsuario = 2;

            // Act
            serviceCotCom.EnviarCotacaoFornecedor(idRequisicao, fornecedores, idUsuario);
            item = serviceCotCom.GetCotacaoFornecedor(idRequisicao, idFornecedor);

            // Assert
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.ReqRequisicaoId == idRequisicao);
            Assert.IsTrue(item.FornecedorId == idFornecedor);
            Assert.IsTrue(item.Preenchida == false);
        }

        [TestMethod()]
        public void getHtmlCotacaoTest()
        {
            // ! This method was private, to test has to change to public

            // Arrange
            int idRequisicao = 1;
            int idFornecedor = 1;
            string html;
            CotCotadoComService ccs = new CotCotadoComService();

            // Act
            html = ccs.getHtmlCotacao(idRequisicao, idFornecedor);

            // Assert
            Assert.IsTrue(html.Length > 0);
        }
    }
}