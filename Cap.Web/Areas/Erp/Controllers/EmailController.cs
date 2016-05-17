using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Cap.Web.Common;
using Cap.Domain.Models.Gen;
using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;

namespace Cap.Web.Areas.Erp.Controllers
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class EmailController : Controller
    {
        IBaseService<AgendaEmail> service;
        ILogin login;

        public EmailController(IBaseService<AgendaEmail> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Email
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Emails(int idAgenda)
        {
            var emails = service.Listar()
                .Where(x => x.IdAgenda == idAgenda)
                .ToList();

            ViewBag.IdAgenda = idAgenda;
            return PartialView(emails);
        }
    }
}