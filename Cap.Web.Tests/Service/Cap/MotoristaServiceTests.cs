using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cap.Domain.Service.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cap.Domain.Models.Cap;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;

namespace Cap.Domain.Service.Cap.Tests
{
    [TestClass()]
    public class MotoristaServiceTests
    {
        private IBaseService<Motorista> service;
        private IBaseService<Usuario> serviceUsuario;

        public MotoristaServiceTests()
        {
            service = new MotoristaService();
            serviceUsuario = new UsuarioService();
        }

        [TestMethod()]
        public void MotoristaGravarTest()
        {
            // Arrange
            Motorista item = new Motorista { UsuarioId = 2 };

            // Act
            service.Gravar(item);

            // Assert
            Assert.IsTrue(item.Id > 0);
        }

        [TestMethod()]
        public void ListarTest()
        {
            // Arrange
            List<Usuario> usuarios;

            // Act
            usuarios = service.Listar().Select(x => x.Usuario).ToList();

            // Assert
            Assert.IsTrue(usuarios.Count() > 0);
        }

        [TestMethod()]
        public void FindTest()
        {
            // Arrange
            string nome;
            Usuario usuario = serviceUsuario.Find(2);

            // Act
            nome = service.Find(1).Usuario.Nome;

            // Assert
            Assert.IsNotNull(nome);
            Assert.IsTrue(usuario.Nome == nome);
        }
    }
}