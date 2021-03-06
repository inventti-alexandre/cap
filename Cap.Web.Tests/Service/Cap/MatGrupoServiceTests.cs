﻿using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Service.Requisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            MatGrupo mat = new MatGrupo { Descricao = "ELETRICA", AlteradoPor = 2, IdEmpresa = 2 };

            // Act
            mat.Id = service.Gravar(mat);

            // Assert
            Assert.IsTrue(mat.Id > 0);
        }
    }
}