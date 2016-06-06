using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.basico
{
    [AreaAuthorizeAttribute("Erp", Roles = "grupo-r")]
    public class GrupoController : Controller
    {
        private IBaseService<Grupo> service;
        private ILogin login;

        public GrupoController(IBaseService<Grupo> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Grupo
        [AreaAuthorizeAttribute("Erp", Roles = "grupo-r")]
        public ActionResult Index()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            var grupos = service.Listar()
                .Where(x => x.IdEmpresa == usuario.IdEmpresa)
                .OrderBy(x => x.Descricao)
                .AsEnumerable();

            return View(grupos);
        }

        // GET: Erp/Grupo/Details/5
        [AreaAuthorizeAttribute("Erp", Roles = "grupo-r")]
        public ActionResult Details(int id)
        {
            var grupo = service.Find(id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // GET: Erp/Grupo/Create
        [AreaAuthorizeAttribute("Erp", Roles = "grupo-c")]
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            return View(new Grupo { AlteradoPor = usuario.Id, IdEmpresa = usuario.IdEmpresa });
        }

        // POST: Erp/Grupo/Create
        [HttpPost]
        [AreaAuthorizeAttribute("Erp", Roles = "grupo-c")]
        public ActionResult Create(Grupo grupo)
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

        // GET: Erp/Grupo/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "grupo-u")]
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

            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            grupo.AlteradoPor = usuario.Id;
            return View(grupo);
        }

        // POST: Erp/Grupo/Edit/5
        [HttpPost]
        [AreaAuthorizeAttribute("Erp", Roles = "grupo-u")]
        public ActionResult Edit(Grupo grupo)
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

        // GET: Erp/Grupo/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "grupo-d")]
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

            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            grupo.AlteradoPor = usuario.Id;
            return View(grupo);
        }

        // POST: Erp/Grupo/Delete/5
        [HttpPost]
        [AreaAuthorizeAttribute("Erp", Roles = "grupo-d")]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                var grupo = service.Find(id);
                if (grupo == null)
                {
                    return HttpNotFound();
                }
                ModelState.AddModelError(string.Empty, e.Message);
                return View(grupo);
            }
        }
    }
}
