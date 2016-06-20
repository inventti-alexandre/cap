using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Email;
using Cap.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers
{
    [AreaAuthorizeAttribute("Erp", Roles = "configuracoes-r")]
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
        public ActionResult Index()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var config = service.Listar().FirstOrDefault(x => x.IdEmpresa == usuario.IdEmpresa);
            if (config != null)
            {
                return PartialView(config);
            }

            ViewBag.Message = string.Empty;
            return PartialView(new EmailConfig { AlteradoPor = usuario.Id, Ativo = true, IdEmpresa = usuario.IdEmpresa, Sender = usuario.Empresa.Email, UseSSL = true });
        }

        [HttpPost]
        [AreaAuthorizeAttribute("Erp", Roles = "configuracoes-u")]
        public ActionResult Index(EmailConfig config)
        {
            config.AlteradoEm = DateTime.Now;
            TryUpdateModel(config);

            if (ModelState.IsValid)
            {
                try
                {
                    config.Id = service.Gravar(config);
                    TempData["Message"] = "Configurações gravadas";
                }
                catch (Exception e)
                {
                    TempData["Message"] = e.Message;
                }
            }
            else
            {
                TempData["Message"] = "Informações inválidas";
            }

            return PartialView(config);
        }
    }
}