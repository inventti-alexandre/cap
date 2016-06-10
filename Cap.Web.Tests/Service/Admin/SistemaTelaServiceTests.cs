using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cap.Domain.Service.Admin.Tests
{
    [TestClass()]
    public class SistemaTelaServiceTests
    {
        private IBaseService<SistemaTela> service;

        public SistemaTelaServiceTests()
        {
            service = new SistemaTelaService();
        }

        [TestMethod()]
        public void SistemaTelaGravarTest()
        {
            // Arrange
            SistemaTela item = new SistemaTela { AlteradoEm = DateTime.Now, AlteradoPor = 2, Descricao = "GRUPOS DE USUÁRIOS", IdSistemaArea = 1, Menu = true, Regra = "grupo", Link = "/ERP/GRUPO", TextoMenu = "Grupos de usuários" };

            // Act
            item.Id = service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}