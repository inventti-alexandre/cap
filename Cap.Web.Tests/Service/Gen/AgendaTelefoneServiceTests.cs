using Cap.Domain.Service.Gen;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Gen.Tests
{
    [TestClass()]
    public class AgendaTelefoneServiceTests
    {
        private IBaseService<AgendaTelefone> service;

        public AgendaTelefoneServiceTests()
        {
            service = new AgendaTelefoneService();
        }

        [TestMethod()]
        public void IncluirAgendaTelefoneTest()
        {
            // Arrange
            AgendaTelefone telefone = new AgendaTelefone { AlteradoPor = 2, IdAgenda = 2, Numero = "99721-8670" };

            // Act
            telefone.Id = service.Gravar(telefone);

            // Assert
            Assert.IsTrue(telefone.Id > 0);
        }
    }
}