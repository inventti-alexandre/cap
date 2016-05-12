﻿using Cap.Domain.Service.Gen;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Gen.Tests
{
    [TestClass()]
    public class AgendaTelefoneServiceTests
    {
        private ILogin<AgendaTelefone> service;

        public AgendaTelefoneServiceTests()
        {
            service = new AgendaTelefoneService();
        }

        [TestMethod()]
        public void IncluirAgendaTelefoneTest()
        {
            // Arrange
            AgendaTelefone telefone = new AgendaTelefone { AlteradoPor = 1, IdAgenda = 1, Numero = "99721-8670" };

            // Act
            telefone.Id = service.Gravar(telefone);

            // Assert
            Assert.IsTrue(telefone.Id > 0);
        }
    }
}