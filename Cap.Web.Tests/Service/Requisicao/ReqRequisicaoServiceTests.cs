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
    public class ReqRequisicaoServiceTests
    {
        private IBaseService<ReqRequisicao> service;

        public ReqRequisicaoServiceTests()
        {
            service = new ReqRequisicaoService();
        }

        [TestMethod()]
        public void ReqRequisicaoGravarTest()
        {
            // Arrange
            var requisicao = new ReqRequisicao { CotarAte = new DateTime(2016, 5, 15), EntregarDia = new DateTime(2016, 5, 17), IdSolicitadoPor = 1, Observ = "teste", IdDepartamento =1, Situacao = Situacao.EmDigitacao };

            // Act
            requisicao.Id = service.Gravar(requisicao);

            // Assert
            Assert.IsTrue(requisicao.Id > 0);
        }
    }
}