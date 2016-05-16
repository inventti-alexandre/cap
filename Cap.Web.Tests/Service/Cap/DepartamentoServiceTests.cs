using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class DepartamentoServiceTests
    {
        IBaseService<Departamento> service;

        public DepartamentoServiceTests()
        {
            service = new DepartamentoService();
        }

        [TestMethod()]
        public void IncluirDepartamento()
        {
            // Arrange
            Departamento departamento = new Departamento
            {
                AlteradoPor = 2,
                Bairro = "PINHEIROS",
                Cep = "05414012",
                Cidade = "SAO PAULO",
                Descricao = "JOAO MOURA",
                Endereco = "RUA JOAO MOURA 123",
                IdEmpresa = 2,
                IdEstado = 1
            };

            // Act
            departamento.Id = service.Gravar(departamento);

            // Assert
            Assert.IsTrue(departamento.Id > 0);
        }
    }
}