using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class InfoCaixaController : Controller
    {
        private IBaseService<InfoCaixa> service;
        private ILogin login;

        public InfoCaixaController(IBaseService<InfoCaixa> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/InfoCaixa
        public ActionResult Index()
        {
            InfoCaixa item = service.Listar().Where(x => x.EmpresaId == GetIdEmpresa()).FirstOrDefault();

            if (item == null)
            {
                item = new InfoCaixa { AlteradoEm = DateTime.Now, DataCaixa = DateTime.Today.Date, EmpresaId = GetIdEmpresa(), UsuarioId = GetIdUsuario() };
            }

            return View(item);
        }

        // POST: Erp/InfoCaixa/Create
        [HttpPost]
        public ActionResult Save(InfoCaixa item)
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
