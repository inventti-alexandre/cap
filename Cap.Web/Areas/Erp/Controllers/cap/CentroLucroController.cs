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
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class CentroLucroController : Controller
    {
        private IBaseService<CentroLucro> service;
        private ILogin login;

        public CentroLucroController(IBaseService<CentroLucro> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/CentroLucro
        public ActionResult Index(int id)
        {
            var centros = service.Listar()
                .Where(x => x.IdGrupoLucro == id)
                .ToList()
                .OrderBy(x => x.GrupoLucro.Descricao)
                .ThenBy(x => x.Descricao)
                .AsEnumerable();

            ViewBag.IdGrupoLucro = id;
            return View(centros);
        }

        // GET: Erp/CentroLucro/Details/5
        public ActionResult Details(int id)
        {
            var centro = service.Find(id);

            if (centro == null)
            {
                return HttpNotFound();
            }

            return View(centro);
        }

        // GET: Erp/CentroLucro/Create
        public ActionResult Create(int idGrupoLucro)
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var centro = new CentroLucro {  IdEmpresa = usuario.IdEmpresa, AlteradoPor = usuario.Id, IdGrupoLucro = idGrupoLucro};
            return View(centro);
        }

        // POST: Erp/CentroLucro/Create
        [HttpPost]
        public ActionResult Create(CentroLucro centro)
        {
            try
            {
                centro.AlteradoEm = DateTime.Now;
                TryUpdateModel(centro);

                if (ModelState.IsValid)
                {
                    service.Gravar(centro);
                    return RedirectToAction("Index", new { id = centro.IdGrupoLucro });
                }

                return View(centro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(centro);
            }
        }

        // GET: Erp/CentroLucro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var centro = service.Find((int)id);

            if (id == null)
            {
                return HttpNotFound();
            }

            centro.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(centro);
        }

        // POST: Erp/CentroLucro/Edit/5
        [HttpPost]
        public ActionResult Edit(CentroLucro centro)
        {
            try
            {
                centro.AlteradoEm = DateTime.Now;
                TryUpdateModel(centro);

                if (ModelState.IsValid)
                {
                    service.Gravar(centro);
                    return RedirectToAction("Index", new { id = centro.IdGrupoLucro });
                }

                return View(centro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(centro);
            }
        }

        // GET: Erp/CentroLucro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var centro = service.Find((int)id);

            if (id == null)
            {
                return HttpNotFound();
            }

            centro.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(centro);
        }

        // POST: Erp/CentroLucro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var centro = service.Excluir(id);
                return RedirectToAction("Index", new { id = centro.IdGrupoLucro });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var centro = service.Find(id);
                if (centro == null)
                {
                    return HttpNotFound();
                }
                return View(centro);
            }
        }
    }
}
