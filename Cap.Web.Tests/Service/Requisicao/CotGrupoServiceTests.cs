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
    public class CotGrupoServiceTests
    {
        private IBaseService<CotGrupo> service;

        public CotGrupoServiceTests()
        {
            service = new CotGrupoService();
        }

        [TestMethod()]
        public void CotGrupoGravarTest()
        {
            // Arrange
            var item = new CotGrupo { Descricao = "CIMENTO", EmpresaId = 2, UsuarioId = 2 };

            // Act
            service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}