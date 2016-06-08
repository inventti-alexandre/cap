using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using Cap.Web.Common;
using System.Web.Mvc;

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
        [AreaAuthorizeAttribute("Erp", Roles = "telaregra-u")]
        public ActionResult Index(int idTela, int[] selecionado)
        {
            service.SetRegras(idTela, selecionado);
            return RedirectToAction("Index", "SistemaTela");
        }
    }
}