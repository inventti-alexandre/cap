using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class CaixaController : Controller
    {
        private ICaixa caixa;
        private ILogin login;

        public CaixaController(ICaixa caixa, ILogin login)
        {
            this.caixa = caixa;
            this.login = login;
        }

        // GET: Erp/Caixa
        public ActionResult Index()
        {
            ViewBag.Inicial = DateTime.Today.Date; // TODO: Data do ultimo caixa fechado + 1 dia
            ViewBag.Final = DateTime.Today.Date; // TODO: Data do proximo caixa - 1 dia

            return View();
        }

        public ActionResult GetParcelas(DateTime inicial, DateTime final, int idDepartamento, int idFornecedor, int idPgto)
        {
            var parcelas = caixa.GetParcelas(login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa, inicial, final, idDepartamento, idFornecedor, idPgto);

            return View(parcelas);
        }
    }
}