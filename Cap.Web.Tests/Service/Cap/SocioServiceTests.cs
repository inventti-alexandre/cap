using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class SocioServiceTests
    {
        private IBaseService<Socio> service;

        public SocioServiceTests()
        {
            service = new SocioService();
        }

        [TestMethod()]
        public void IncluirSocio()
        {
            // Arrange
            Socio socio = new Socio
            {
                AlteradoPor = 1,
                Bairro = "MIRANDOPOLIS",
                Cep = "04043012",
                Cidade = "SAO PAULO",
                Cpf = "12557634859",
                Email = "jb.alessandro@gmail.com",
                Endereco = "RUA LUIS GOIS 1850 AP 12",
                IdEmpresa = 2,
                IdEstado = 1,
                IdEstadoCivil = 6,
                Nascimento = new DateTime(1972, 6, 6),
                Nacionalidade = "BRASILEIRA",
                Nome = "JOSE ALESSANDRO",
                Profissao = "ANALISTA DE SISTEMAS",
                Telefone = "997218670"
            };

            // Act
            socio.Id = service.Gravar(socio);

            // Assert
            Assert.IsTrue(socio.Id > 0);
        }
    }
}