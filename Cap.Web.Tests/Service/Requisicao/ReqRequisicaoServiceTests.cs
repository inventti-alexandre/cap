using Cap.Domain.Service.Requisicao;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Cap.Domain.Abstract.Req;
using System.Collections.Generic;

namespace Cap.Domain.Service.Requisicao.Tests
{
    [TestClass()]
    public class ReqRequisicaoServiceTests
    {
        private IBaseService<ReqRequisicao> service;
        private IRequisicao serviceRequisicao;

        public ReqRequisicaoServiceTests()
        {
            service = new ReqRequisicaoService();
            serviceRequisicao = new ReqRequisicaoService();
        }

        [TestMethod()]
        public void ReqRequisicaoGravarTest()
        {
            // Arrange
            var requisicao = new ReqRequisicao { CotarAte = new DateTime(2016, 5, 15), EntregarDia = new DateTime(2016, 5, 17), IdSolicitadoPor = 2, Observ = "teste", IdDepartamento = 1, Situacao = Situacao.EmDigitacao };

            // Act
            requisicao.Id = service.Gravar(requisicao);

            // Assert
            Assert.IsTrue(requisicao.Id > 0);
        }

        [TestMethod()]
        public void GetRequisicoesTest()
        {
            // Arrange
            List<ReqRequisicao> requisicoes;
            DateTime inicial = DateTime.Today.Date.AddDays(-5);

            // Act
            requisicoes = serviceRequisicao.GetRequisicoes(Situacao.Comprada, 2, 0, inicial);

            // Assert
            Assert.IsTrue(requisicoes.Count > 0);
        }
    }
}