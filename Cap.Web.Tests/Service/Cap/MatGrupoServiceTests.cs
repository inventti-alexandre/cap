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
    public class MatGrupoServiceTests
    {
        private IBaseService<MatGrupo> service;

        public MatGrupoServiceTests()
        {
            service = new MatGrupoService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            MatGrupo mat = new MatGrupo { Descricao = "ELETRICA", AlteradoPor = 1 };

            // Act
            mat.Id = service.Gravar(mat);

            // Assert
            Assert.IsTrue(mat.Id > 0);
        }
    }
}