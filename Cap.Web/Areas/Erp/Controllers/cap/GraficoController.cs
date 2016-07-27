using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Cap;
using System;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class GraficoController : Controller
    {
        private IGrafico service;
        private ISistemaConfig config;
        private ILogin login;

        public GraficoController(IGrafico service, ILogin login, ISistemaConfig config)
        {
            this.service = service;
            this.login = login;
            this.config = config;
        }

        // GET: Erp/Grafico
        public ActionResult Index()
        {
            int idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
            ViewBag.Dias = config.GetConfig(idEmpresa).GraficoDias;

            return View();
        }

        public ActionResult GetGrafico(int dias, int idDepartamento, int idPgto)
        {
            try
            {
                int idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
                DateTime inicial = DateTime.Today.Date;
                DateTime final = inicial.AddDays(dias);
                var grafico = service.GetGrafico(inicial, final, idEmpresa, idDepartamento, idPgto);
                return PartialView(grafico);
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}