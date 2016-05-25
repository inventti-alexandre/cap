using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Models.Cap;
using Cap.Domain.Abstract;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class GrupoLucroServiceTests
    {
        private IBaseService<GrupoLucro> service;

        public GrupoLucroServiceTests()
        {
            service = new GrupoLucroService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            GrupoLucro grupo = new GrupoLucro { IdEmpresa = 2, AlteradoPor = 2, Descricao = "VENDAS" };

            // Act
            grupo.Id = service.Gravar(grupo);

            // Assert
            Assert.IsTrue(grupo.Id > 0);
        }
    }
}