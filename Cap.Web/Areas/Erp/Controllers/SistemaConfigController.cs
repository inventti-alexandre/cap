using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using System;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers
{
    public class SistemaConfigController : Controller
    {
        ISistemaConfig service;
        ILogin login;

        public SistemaConfigController(ISistemaConfig service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/SistemaConfig
        public ActionResult Index()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var config = service.GetConfig(usuario.IdEmpresa);

            return PartialView(config);
        }

        // POST: Erp/SistemaConfig
        [HttpPost]
        public ActionResult Index(SistemaConfig config)
        {
            try
            {
                var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

                if (ModelState.IsValid)
                {
                    service.SetConfig(config, usuario.Id);
                    ViewBag.Message = "Configurações gravadas";
                }

                return PartialView(config);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return PartialView(config);
            }            
        }
    }
}