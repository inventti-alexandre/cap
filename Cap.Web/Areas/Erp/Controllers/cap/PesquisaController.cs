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
    [AreaAuthorizeAttribute("Erp", Roles = "pesquisa-r")]
    public class PesquisaController : Controller
    {
        IBaseService<Parcela> service;
        ILogin login;

        public PesquisaController(IBaseService<Parcela> service,ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Pesquisa
        public ActionResult Index()
        {
            return View(new PesquisaModel());
        }

        public ActionResult Pesquisar(PesquisaModel pesquisa)
        {
            try
            {
                var model = new PesquisaService().Pesquisar(pesquisa);
                if (model.Count() > 0)
                {
                    @ViewBag.ValorTotal = model.Sum(x => x.Valor).ToString("c2");
                    @ViewBag.ValorPago = model.Where(x => x.Pago == true).Sum(x => x.Valor).ToString("c2");
                    @ViewBag.ValorAPagar = model.Where(x => x.Pago == false).Sum(x => x.Valor).ToString("c2");
                }

                return PartialView(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(pesquisa);
            }
        }

        public ActionResult Details(int id)
        {
            var parcela = service.Find(id);
            return PartialView(parcela);
        }

    }
}
