using Cap.Domain.Abstract.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;

namespace Cap.Web.Areas.Erp.Controllers.basico
{
    [AreaAuthorizeAttribute("Erp", Roles = "telaregra-r")]
    public class TelaRegraController : Controller
    {
        private ITelaRegra service;
        private IBaseService<SistemaTela> serviceTela;

        public TelaRegraController(ITelaRegra service, IBaseService<SistemaTela> serviceTela)
        {
            this.service = service;
            this.serviceTela = serviceTela;
        }

        // GET: Erp/TelaRegra
        public ActionResult Index(int idTela)
        {
            ViewBag.IdTela = idTela;
            ViewBag.Tela = serviceTela.Find(idTela).Descricao;
            return View(service.GetRegras(idTela));
        }

        // POST: Erp/TelaRegra
        [HttpPost]
        public ActionResult Index(int idTela, int[] selecionado)
        {
            service.SetRegras(idTela, selecionado);
            return RedirectToAction("Index", "SistemaTela");
        }
    }
}