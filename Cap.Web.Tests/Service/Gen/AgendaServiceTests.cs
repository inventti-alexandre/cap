using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Models.Gen;
using Cap.Domain.Abstract;

namespace Cap.Domain.Service.Gen.Tests
{
    [TestClass()]
    public class AgendaServiceTests
    {
        private IBaseService<Agenda> service;

        public AgendaServiceTests()
        {
            service = new AgendaService();
        }

        [TestMethod()]
        public void GravarTest()
        {
            // Arrange
            Agenda agenda = new Agenda { AlteradoPor = 1, IdEstado = 1, Nome = "JOSE ALESSANDRO" };

            // Act
            agenda.Id = service.Gravar(agenda);

            // Assert
            Assert.IsTrue(agenda.Id > 0);
        }
    }
}