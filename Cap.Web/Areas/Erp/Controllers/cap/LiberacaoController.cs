using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class LiberacaoController : Controller
    {
        private ILiberacao service;
        private ILogin login;

        public LiberacaoController(ILiberacao service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Liberacao
        public ActionResult Index(DateTime? final)
        {
            final = getFinal(final);

            ViewBag.Final = final;
            return View();
        }
        
        public PartialViewResult ParcelasALiberar(DateTime? final)
        {
            final = getFinal(final);

            var parcelas = getParcelas((DateTime)final);

            if (parcelas.Count == 0)
            {
                ViewBag.Message = $"Nenhuma parcela carente de liberação até dia {((DateTime)final).ToShortDateString() }";
            }
            else
            {
                ViewBag.Message = string.Empty;
            }

            return PartialView(parcelas);
        }

        public ActionResult LiberarParcelas(int[] selecionado, DateTime? final)
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            service.LiberarParcelas(selecionado.ToList(), idUsuario);

            final = getFinal(final);

            return RedirectToAction("ParcelasALiberar", new { final = final });
        }

        public ActionResult CancelarLiberacao(int idParcela)
        {
            try
            {
                var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                service.CancelarLiberacao(idParcela, idUsuario);
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<Parcela> getParcelas(DateTime final)
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return service.ParcelasALiberar(idUsuario, final);
        }

        private DateTime getFinal(DateTime? final)
        {
            // TODO: parametro do sistema (o certo eh alterar em LiberacaoService)

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