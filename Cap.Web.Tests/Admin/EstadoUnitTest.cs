using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;

namespace Cap.Web.Tests.Admin
{
    [TestClass]
    public class EstadoUnitTest
    {
        private IBaseService<Estado> service;

        public EstadoUnitTest()
        {
            service = new EstadoService();
        }

        [TestMethod]
        public void IncluirEstados()
        {
            // Arrange
            var estado01 = new Estado { Ativo = true, Descricao = "ACRE", UF = "AC" };
            var estado02 = new Estado { Ativo = true, Descricao = "ALAGOAS", UF = "AL" };
            var estado03 = new Estado { Ativo = true, Descricao = "AMAPA", UF = "AP" };
            var estado04 = new Estado { Ativo = true, Descricao = "AMAZONAS", UF = "AM" };
            var estado05 = new Estado { Ativo = true, Descricao = "BAHIA", UF = "BA" };
            var estado06 = new Estado { Ativo = true, Descricao = "CEARA", UF = "CE" };
            var estado07 = new Estado { Ativo = true, Descricao = "DISTRITO FEDERAL", UF = "DF" };
            var estado08 = new Estado { Ativo = true, Descricao = "ESPIRITO SANTO", UF = "ES" };
            var estado09 = new Estado { Ativo = true, Descricao = "GOIAS", UF = "GO" };
            var estado10 = new Estado { Ativo = true, Descricao = "MARANHAO", UF = "MA" };
            var estado11 = new Estado { Ativo = true, Descricao = "MATO GROSSO", UF = "MT" };
            var estado12 = new Estado { Ativo = true, Descricao = "MATO GROSSO DO SUL", UF = "MS" };
            var estado13 = new Estado { Ativo = true, Descricao = "MINAS GERAIS", UF = "MG" };
            var estado14 = new Estado { Ativo = true, Descricao = "PARA", UF = "PA" };
            var estado15 = new Estado { Ativo = true, Descricao = "PARAIBA", UF = "PB" };
            var estado16 = new Estado { Ativo = true, Descricao = "PARANA", UF = "PR" };
            var estado17 = new Estado { Ativo = true, Descricao = "PERNAMBUCO", UF = "PE" };
            var estado18 = new Estado { Ativo = true, Descricao = "PIAUI", UF = "PI" };
            var estado19 = new Estado { Ativo = true, Descricao = "RIO DE JANEIRO", UF = "RJ" };
            var estado20 = new Estado { Ativo = true, Descricao = "RIO GRANDE DO NORTE", UF = "RN" };
            var estado21 = new Estado { Ativo = true, Descricao = "RIO GRANDE DO SUL", UF = "RS" };
            var estado22 = new Estado { Ativo = true, Descricao = "RONDONIA", UF = "RO" };
            var estado23 = new Estado { Ativo = true, Descricao = "RORAIMA", UF = "RR" };
            var estado24 = new Estado { Ativo = true, Descricao = "SANTA CATARINA", UF = "SC" };
            var estado25 = new Estado { Ativo = true, Descricao = "SAO PAULO", UF = "SP" };
            var estado26 = new Estado { Ativo = true, Descricao = "SERGIPE", UF = "SE" };
            var estado27 = new Estado { Ativo = true, Descricao = "TOCANTINS", UF = "TO" };

            // Act
            estado01.Id = service.Gravar(estado01);
            estado02.Id = service.Gravar(estado02);
            estado03.Id = service.Gravar(estado03);
            estado04.Id = service.Gravar(estado04);
            estado05.Id = service.Gravar(estado05);
            estado06.Id = service.Gravar(estado06);
            estado07.Id = service.Gravar(estado07);
            estado08.Id = service.Gravar(estado08);
            estado09.Id = service.Gravar(estado09);
            estado10.Id = service.Gravar(estado10);
            estado11.Id = service.Gravar(estado11);
            estado12.Id = service.Gravar(estado12);
            estado13.Id = service.Gravar(estado13);
            estado14.Id = service.Gravar(estado14);
            estado15.Id = service.Gravar(estado15);
            estado16.Id = service.Gravar(estado16);
            estado17.Id = service.Gravar(estado17);
            estado18.Id = service.Gravar(estado18);
            estado19.Id = service.Gravar(estado19);
            estado20.Id = service.Gravar(estado20);
            estado21.Id = service.Gravar(estado21);
            estado22.Id = service.Gravar(estado22);
            estado23.Id = service.Gravar(estado23);
            estado24.Id = service.Gravar(estado24);
            estado25.Id = service.Gravar(estado25);
            estado26.Id = service.Gravar(estado26);
            estado27.Id = service.Gravar(estado27);

            // Assert
            Assert.IsTrue(estado01.Id > 0);
            Assert.IsTrue(estado02.Id > 0);
            Assert.IsTrue(estado03.Id > 0);
            Assert.IsTrue(estado04.Id > 0);
            Assert.IsTrue(estado05.Id > 0);
            Assert.IsTrue(estado06.Id > 0);
            Assert.IsTrue(estado07.Id > 0);
            Assert.IsTrue(estado08.Id > 0);
            Assert.IsTrue(estado09.Id > 0);
            Assert.IsTrue(estado10.Id > 0);
            Assert.IsTrue(estado11.Id > 0);
            Assert.IsTrue(estado12.Id > 0);
            Assert.IsTrue(estado13.Id > 0);
            Assert.IsTrue(estado14.Id > 0);
            Assert.IsTrue(estado15.Id > 0);
            Assert.IsTrue(estado16.Id > 0);
            Assert.IsTrue(estado17.Id > 0);
            Assert.IsTrue(estado18.Id > 0);
            Assert.IsTrue(estado19.Id > 0);
            Assert.IsTrue(estado20.Id > 0);
            Assert.IsTrue(estado21.Id > 0);
            Assert.IsTrue(estado22.Id > 0);
            Assert.IsTrue(estado23.Id > 0);
            Assert.IsTrue(estado24.Id > 0);
            Assert.IsTrue(estado25.Id > 0);
            Assert.IsTrue(estado26.Id > 0);
            Assert.IsTrue(estado27.Id > 0);
        }
    }
}
