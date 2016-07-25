using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class BoletoController : Controller
    {
        private IBoleto service;
        private ILogin login;

        public BoletoController(IBoleto service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Boleto
        public ActionResult Index(DateTime? inicial, DateTime? final)
        {
            inicial = getDataInicial(inicial);
            final = getDataFinal(final);

            ViewBag.Inicial = inicial;
            ViewBag.Final = final;
            return View();
        }

        // GET: Erp/Boleto/GetParcelas
        public ActionResult GetParcelas(string inicial, string final)
        {
            try
            {
                DateTime vInicial;
                if (!DateTime.TryParse(inicial, out vInicial))
                {
                    vInicial = DateTime.MinValue;
                }

                DateTime vFinal;
                if (!DateTime.TryParse(final, out vFinal))
                {
                    vFinal = DateTime.MinValue;
                }

                var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                var parcelas = service.GetBoletos(usuario.IdEmpresa, vInicial, vFinal);

                return PartialView(parcelas);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(new List<Parcela>());
            }
        }

        private DateTime? getDataFinal(DateTime? final)
        {
            if (final == null || final == DateTime.MinValue)
            {
                final = DateTime.Today.Date;
            }

            return final;
        }

        private static DateTime? getDataInicial(DateTime? inicial)
        {
            if (inicial == null || inicial == DateTime.MinValue)
            {
                if (DateTime.Today.Date.DayOfWeek == DayOfWeek.Monday)
                {
                    inicial = DateTime.Today.Date.AddDays(-2);
                }
                else if (DateTime.Today.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    inicial = DateTime.Today.Date.AddDays(-1);
                }
                else
                {
                    inicial = DateTime.Today.Date;
                }
            }

            return inicial;
        }
    }
}