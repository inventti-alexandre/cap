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
    public class ContaServiceTests
    {
        private IBaseService<Conta> service;

        public ContaServiceTests()
        {
            service = new ContaService();
        }

        [TestMethod()]
        public void ContaGravarTest()
        {
            // Arrange
            Conta conta = new Conta() { Agencia = "0190", AgenciaNome = "PARAISO", AlteradoEm = DateTime.Now, AlteradoPor = 2, ChequeAtual = 1, ContaNumero = "106757", DataSaldo = new DateTime(2016, 5, 19), DataSaldoAnterior = new DateTime(2016, 5, 18), Descricao = "ITAU ATLANTICA", IdBanco = 1, IdContaTipo = 1, Observ = "", Saldo = 100, SaldoAnterior = 10, IdEmpresa = 1 };

            // Act
            service.Gravar(conta);

            // Assert
            Assert.IsTrue(conta.Id > 0);
        }

        [TestMethod()]
        public void ListarTest()
        {
            // Arrange
            List<Conta> contas;

            // Act
            contas = service.Listar().Where(x => x.IdEmpresa == 2).ToList();

            // Assert
            Assert.IsTrue(contas.Count() > 0);
        }
    }
}