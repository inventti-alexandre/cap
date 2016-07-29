using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class InfoCaixaController : Controller
    {
        private IBaseService<InfoCaixa> service;
        private IBaseService<Feriado> feriado;
        private ILogin login;
        private IInfoCaixa info;

        public InfoCaixaController(IBaseService<InfoCaixa> service, ILogin login, IBaseService<Feriado> feriado, IInfoCaixa info)
        {
            this.service = service;
            this.login = login;
            this.feriado = feriado;
            this.info = info;
        }

        // GET: Erp/InfoCaixa
        public ActionResult Index()
        {
            InfoCaixa item = info.GetInfoCaixa(GetIdEmpresa(), GetIdUsuario());

            return View(item);
        }

        // POST: Erp/InfoCaixa/Create
        [HttpPost]
        public ActionResult Index(InfoCaixa item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true });
                }
                return View(item);
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        private int GetIdEmpresa()
        {
            return login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
        }

        private int GetIdUsuario()
        {
            return login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
        }
    }
}
