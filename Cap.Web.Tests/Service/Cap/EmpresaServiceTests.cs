using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class EmpresaServiceTests
    {
        private IBaseService<Empresa> service;

        public EmpresaServiceTests()
        {
            service = new EmpresaService();
        }

        [TestMethod()]
        public void IncluirEmpresa()
        {
            // Arrange
            Empresa empresa = new Empresa
            {
                AlteradoPor = 1,                                
                Bairro = "PINHEIROS",
                Cidade = "SAO PAULO",
                Cep = "05414012",
                Cnpj = "61756995000100",
                Email = "atlantica@construtoraatlantica.com.br",
                Endereco = "RU JOAO MOURA 661 SALA 19",
                Fantasia = "ATLANTICA",
                IdEstado = 1,
                IE = "104942855114",
                Razao = "CONSTR E INCORP ATLANTICA LTDA",
                Telefone = "3146-1212"
            };

            // Act
            empresa.Id = service.Gravar(empresa);

            // Assert
            Assert.IsTrue(empresa.Id > 0);
        }
    }
}