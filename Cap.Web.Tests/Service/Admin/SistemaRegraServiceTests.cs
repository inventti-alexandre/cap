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
    public class SistemaRegraServiceTests
    {
        IBaseService<SistemaRegra> service;

        public SistemaRegraServiceTests()
        {
            service = new SistemaRegraService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            SistemaRegra regraUsuarioC = new SistemaRegra { AlteradoPor = 2, Descricao = "incluir usuario", Sufixo = "c" };
            SistemaRegra regraUsuarioR = new SistemaRegra { AlteradoPor = 2, Descricao = "ler usuario", Sufixo = "r" };
            SistemaRegra regraUsuarioU = new SistemaRegra { AlteradoPor = 2, Descricao = "alterar usuario", Sufixo = "u" };
            SistemaRegra regraUsuarioD = new SistemaRegra { AlteradoPor = 2, Descricao = "excluir usuario", Sufixo = "d" };

            // Act
            regraUsuarioC.Id = service.Gravar(regraUsuarioC);
            regraUsuarioR.Id = service.Gravar(regraUsuarioR);
            regraUsuarioU.Id = service.Gravar(regraUsuarioU);
            regraUsuarioD.Id = service.Gravar(regraUsuarioD);

            // Assert
            Assert.IsTrue(regraUsuarioC.Id > 0);
            Assert.IsTrue(regraUsuarioR.Id > 0);
            Assert.IsTrue(regraUsuarioU.Id > 0);
            Assert.IsTrue(regraUsuarioD.Id > 0);
        }
    }
}