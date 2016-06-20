using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers
{
    public class EmailConfigController : Controller
    {
        IBaseService<EmailConfig> service;
        ILogin login;

        public EmailConfigController(IBaseService<EmailConfig> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/EmailConfig
        [ChildActionOnly]
        [HttpPost]
        public ActionResult Index()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var config = service.Listar().FirstOrDefault(x => x.IdEmpresa == usuario.IdEmpresa);
            if (config != null)
            {
                return PartialView(config);
            }

            return PartialView(new EmailConfig { AlteradoPor = usuario.Id, Ativo = true, IdEmpresa = usuario.IdEmpresa, Sender = usuario.Empresa.Email, UseSSL = true });
        }

        [ChildActionOnly]
        public ActionResult Index(EmailConfig config)
        {
            config.AlteradoEm = DateTime.Now;
            TryUpdateModel(config);

            if (ModelState.IsValid)
            {
                config.Id = service.Gravar(config);
                ViewBag.Message = "Configurações gravadas";
            }

            return PartialView(config);
        }
    }
}