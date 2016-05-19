using Cap.Web.Areas.Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class PesquisaController : Controller
    {
        // GET: Erp/Pesquisa
        public ActionResult Index()
        {
            return View(new PesquisaModel());
        }

        public ActionResult Pesquisa(PesquisaModel pesquisa)
        {
            // TODO: retorna lista de parcelas

            return View();
        }

    }
}
