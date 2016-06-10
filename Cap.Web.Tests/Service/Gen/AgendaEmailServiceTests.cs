using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;

namespace Cap.Domain.Service.Gen.Tests
{
    [TestClass()]
    public class AgendaEmailServiceTests
    {
        private IBaseService<AgendaEmail> service;

        public AgendaEmailServiceTests()
        {
            service = new AgendaEmailService();
        }

        [TestMethod()]
        public void IncluirAgendaEmailTest()
        {
            // Arrange
            AgendaEmail email = new AgendaEmail
            {
                AlteradoPor = 2,
                Email = "jb.alessandro@gmail.com",
                IdAgenda = 1,
            };

            // Act
            email.Id = service.Gravar(email);

            // Assert
            Assert.IsTrue(email.Id > 0);
        }
    }
}