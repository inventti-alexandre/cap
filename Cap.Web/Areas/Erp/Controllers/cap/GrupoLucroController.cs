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
    [AreaAuthorizeAttribute("Erp", Roles = "grupolucro-r")]
    public class GrupoLucroController : Controller
    {
        private IBaseService<GrupoLucro> service;
        private ILogin login;

        public GrupoLucroController(IBaseService<GrupoLucro> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/GrupoLucro
        public ActionResult Index()
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var grupos = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(grupos);
        }

        // GET: Erp/GrupoLucro/Details/5
        public ActionResult Details(int id)
        {
            var grupo = service.Find(id);

            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }

        // GET: Erp/GrupoLucro/Create
        [AreaAuthorizeAttribute("Erp", Roles = "grupolucro-c")]
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            return View(new GrupoLucro { IdEmpresa = usuario.IdEmpresa, AlteradoPor = usuario.Id });
        }

        // POST: Erp/GrupoLucro/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdEmpresa,Descricao,AlteradoPor")] GrupoLucro grupo)
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

        // GET: Erp/GrupoLucro/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "grupolucro-u")]
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

        // POST: Erp/GrupoLucro/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,IdEmpresa,Descricao,AlteradoPor")] GrupoLucro grupo)
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

        // GET: Erp/GrupoLucro/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "grupolucro-d")]
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

        // POST: Erp/GrupoLucro/Delete/5
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
