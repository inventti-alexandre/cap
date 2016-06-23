using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class LogisticaServiceTests
    {
        private IBaseService<Logistica> service;

        public LogisticaServiceTests()
        {
            service = new LogisticaService();
        }

        [TestMethod()]
        public void LogisticaGravarTest()
        {
            // Arrange
            var item = new Logistica { DataServico = DateTime.Now, EmpresaId = 2, MotoristaId = 1, Servico = "Levar carro para emplacar", UsuarioId = 2 };

            // Act
            service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }
    }
}