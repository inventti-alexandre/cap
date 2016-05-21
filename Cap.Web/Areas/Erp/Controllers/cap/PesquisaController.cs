using Cap.Web.Areas.Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;
using Cap.Domain.Models.Cap;
using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Service.Cap;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class PesquisaController : Controller
    {
        ILogin login;

        public PesquisaController(ILogin login)
        {
            this.login = login;
        }

        // GET: Erp/Pesquisa
        public ActionResult Index()
        {
            return View(new PesquisaModel());
        }

        public ActionResult Pesquisar(PesquisaModel pesquisa)
        {
            return PartialView("~/Areas/Erp/Views/Parcela/Parcelas.cshtml", new PesquisaService().Pesquisar(pesquisa));
        }

    }
}
