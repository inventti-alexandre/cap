using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Admin;
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
        private IInfoCaixa infoCaixaService;

        public CaixaController(ICaixa caixa, ILogin login, IInfoCaixa infoCaixaService)
        {
            this.caixa = caixa;
            this.login = login;
            this.infoCaixaService = infoCaixaService;
        }

        // GET: Erp/Caixa
        public ActionResult Index()
        {
            try
            {
                var usuario = getUsuario();
                var info = infoCaixaService.GetInfoCaixa(usuario.IdEmpresa, usuario.Id);

                return View(info);
            }
            catch (Exception e)
            {
                return Json(new { error = "Não foi possível listar parcelas: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetParcelas(string inicial, string final, int idDepartamento, int idFornecedor, int idPgto)
        {
            try
            {
                var usuario = getUsuario();
                var info = infoCaixaService.GetInfoCaixa(usuario.IdEmpresa, usuario.Id);

                DateTime dInicial;
                if (!DateTime.TryParse(inicial, out dInicial))
                {
                    dInicial = info.DataUltimoCaixa;   
                }

                DateTime dFinal;
                if (!DateTime.TryParse(final, out dFinal))
                {
                    dFinal = info.DataProximoCaixa;
                }

                var parcelas = caixa.GetParcelas(usuario.IdEmpresa, dInicial, dFinal, idDepartamento, idFornecedor, idPgto);

                return PartialView(parcelas);
            }
            catch (Exception e)
            {
                return Json(new { error = "Não foi possível listar parcelas: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private Usuario getUsuario()
        {
            return login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
        }
    }
}