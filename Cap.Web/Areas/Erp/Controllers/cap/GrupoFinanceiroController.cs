using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "grupofinanceiro-r")]
    public class GrupoFinanceiroController : Controller
    {
        private IBaseService<GrupoFinanceiro> service;
        private ILogin login;

        public GrupoFinanceiroController(IBaseService<GrupoFinanceiro> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/GrupoFinanceiro
        public ActionResult Index()
        {
            int idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var grupos = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .ToList();

            return View(grupos);
        }

        // GET: Erp/GrupoFinanceiro/Details/5
        public ActionResult Details(int id)
        {
            var grupo = service.Find(id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // GET: Erp/GrupoFinanceiro/Create
        [AreaAuthorizeAttribute("Erp", Roles = "grupofinanceiro-c")]
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            return View(new GrupoFinanceiro { AlteradoPor = usuario.Id, IdEmpresa = usuario.IdEmpresa });
        }

        // POST: Erp/GrupoFinanceiro/Create
        [HttpPost]
        public ActionResult Create(GrupoFinanceiro grupo)
        {
            try
            {
                grupo.AlteradoEm = DateTime.Now;
                TryUpdateModel(grupo);

                if (ModelState.IsValid)
                {
                    service.Gravar(grupo);
                    return RedirectToAction("Index");
                }

                return View(grupo);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }

        // GET: Erp/GrupoFinanceiro/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "grupofinanceiro-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = service.Find((int)id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // POST: Erp/GrupoFinanceiro/Edit/5
        [HttpPost]
        public ActionResult Edit(GrupoFinanceiro grupo)
        {
            try
            {
                grupo.AlteradoEm = DateTime.Now;
                TryUpdateModel(grupo);

                if (ModelState.IsValid)
                {
                    service.Gravar(grupo);
                    return RedirectToAction("Index");
                }

                return View(grupo);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }

        // GET: Erp/GrupoFinanceiro/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "grupofinanceiro-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = service.Find((int)id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // POST: Erp/GrupoFinanceiro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var grupo = service.Find(id);
                if (grupo == null)
                {
                    return HttpNotFound();
                }
                return View(grupo);
            }
        }
    }
}
