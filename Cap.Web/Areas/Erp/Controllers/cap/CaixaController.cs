using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Web.Areas.Erp.Models;
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
        private IBaseService<Conta> contaService;

        public CaixaController(ICaixa caixa, ILogin login, IInfoCaixa infoCaixaService, IBaseService<Conta> contaService)
        {
            this.caixa = caixa;
            this.login = login;
            this.infoCaixaService = infoCaixaService;
            this.contaService = contaService;
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

        // GET: Erp/Caixa/BaixarParcelasSelecionadas/[5]
        public ActionResult BaixarParcelasSelecionadas(int[] selecionados, int idConta)
        {
            try
            {
                var item = new BaixarParcelasSelecionadasModel
                {
                    Cheque = getProximoCheque(idConta),
                    DataCompensacao = getDataCaixa().ToShortDateString(),
                    IdConta = idConta,
                    Selecionados = selecionados.ToList()
                };

                return View(item);
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        private DateTime getDataCaixa()
        {
            var usuario = getUsuario();
            return infoCaixaService.GetInfoCaixa(usuario.IdEmpresa, usuario.Id).DataCaixa;
        }

        private int getProximoCheque(int idConta)
        {
            var usuario = getUsuario();
            return contaService.Find(idConta).ProximoCheque;
        }

        // POST: Erp/Caixa/BaixarParcelasSelecionadas/[5]
        [HttpPost]
        public ActionResult BaixarParcelasSelecionadas(int[] selecionados, int idConta, int idCheque, string dataCompensacao)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Usuario getUsuario()
        {
            return login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
        }
    }
}