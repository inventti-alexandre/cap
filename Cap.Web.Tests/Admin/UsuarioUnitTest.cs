using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cap.Web.Tests.Admin
{
    /// <summary>
    /// Summary description for UsuarioUnitTest
    /// </summary>
    [TestClass]
    public class UsuarioUnitTest
    {
        private ILogin<Usuario> service;

        public UsuarioUnitTest()
        {
            service = new UsuarioService();
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void IncluirUsuario()
        {
            // Arrange
            var usuario = new Usuario
            {
                Ativo = true,
                CadastradoEm = DateTime.Now,
                Email = "jb.alessandro@gmail.com",
                Login = "jose",
                Nome = "JOSE ALESSANDRO",
                Ramal = "",
                Roles = "",
                Senha = "b8c7p2c6",
                Telefone = "99721-8670"
            };

            // Act
            usuario.Id = service.Gravar(usuario);

            // Assert
            Assert.IsTrue(usuario.Id > 0);
        }
    }
}
