using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Models.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Service.Requisicao;

namespace Cap.Domain.Models.Requisicao.Tests
{
    [TestClass()]
    public class ResumoCotacaoTests
    {
        private IResumoCotacao service;

        public ResumoCotacaoTests()
        {
            service = new ResumoCotacao(new ReqRequisicaoService(), new CotCotacaoService());
        }

        [TestMethod()]
        public void GetResumoTest()
        {
            // Arrange
            Resumo resumo;
            int idRequisicao = 1;

            // Act
            resumo = service.GetResumo(idRequisicao);

            // Assert
            Assert.IsTrue(resumo.Indicacao.Count() > 0);
            Assert.IsTrue(resumo.Influencia.Count() > 0);
            Assert.IsTrue(resumo.PrecoMinimo > 0);
            Assert.IsTrue(resumo.Requisicao != null);
            Assert.IsTrue(resumo.ResumoDetalhado.Count() > 1);            
        }
    }
}