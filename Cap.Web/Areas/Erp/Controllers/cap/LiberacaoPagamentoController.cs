using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Cap;
using Cap.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "liberacaopagamento-r")]
    public class LiberacaoPagamentoController : Controller
    {
        private ILiberacaoPagamento service;
        private ILogin login;

        public LiberacaoPagamentoController(ILiberacaoPagamento service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/LiberacaoPagamento
        public ActionResult Index(DateTime? final)
        {
            final = getFinal(final);

            ViewBag.Final = final;
            return View();
        }

        public PartialViewResult ParcelasALiberar(DateTime ? final)
        {
            final = getFinal(final);

            var parcelas = getParcelas((DateTime)final);

            if (parcelas.Count == 0)
            {
                ViewBag.Message = $"Nenhuma parcela carente de liberação até o dia { ((DateTime)final).ToShortDateString() }";
            }
            else
            {
                ViewBag.Message = string.Empty;
            }

            return PartialView(parcelas);
        }

        [AreaAuthorizeAttribute("Erp", Roles = "liberacaopagamento-l")]
        public ActionResult LiberarParcelas(int[] selecionado, DateTime? final)
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            service.LiberarParcelas(selecionado.ToList(), idUsuario);

            final = getFinal(final);

            return RedirectToAction("ParcelasALiberar", new { final = final });
        }

        [AreaAuthorizeAttribute("Erp", Roles = "liberacaopagamento-d")]
        public ActionResult Cancelar(DateTime? final)
        {
            final = getFinal(final);

            ViewBag.Final = final;
            return View();
        }

        [HttpPost]
        public ActionResult Cancelar(int idParcela, DateTime? final)
        {
            try
            {
                var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                service.CancelarLiberacao(idParcela, idUsuario);
                return RedirectToAction("ParcelasACancelar", new { final = final });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PartialViewResult ParcelasACancelar(DateTime? final)
        {
            final = getFinal(final);

            var parcelas = service.ParcelasACancelar(final);

            if (parcelas.Count == 0)
            {
                ViewBag.Message = $"Nenhuma parcela passível de cancelamento de liberação até o dia { ((DateTime)final).ToShortDateString() }";
            }
            else
            {
                ViewBag.Message = string.Empty;
            }

            return PartialView(parcelas);
        }

        private List<Parcela> getParcelas(DateTime final)
        {
            return service.ParcelasALiberar(final);
        }

        private List<Parcela> getParcelasACancelar(DateTime final)
        {
            return service.ParcelasACancelar(final);
        }

        private DateTime getFinal(DateTime? final)
        {
            if (final == null)
            {
                final = DateTime.Today.Date.AddDays(7);
            }

            if (final < DateTime.Today.Date)
            {
                final = DateTime.Today.Date.AddDays(7);
            }

            return (DateTime)final;

        }
    }
}