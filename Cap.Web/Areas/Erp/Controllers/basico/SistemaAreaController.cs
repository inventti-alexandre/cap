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
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class SistemaAreaController : Controller
    {
        private IBaseService<SistemaArea> service;
        private ILogin login;

        public SistemaAreaController(IBaseService<SistemaArea> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/SistemaArea
        public ActionResult Index()
        {
            var areas = service.Listar()
                .OrderBy(x => x.Descricao)
                .AsEnumerable();

            return View(areas);
        }

        // GET: Erp/SistemaArea/Details/5
        public ActionResult Details(int id)
        {
            var area = service.Find(id);

            if (area == null)
            {
                return HttpNotFound();
            }

            return View(area);
        }

        // GET: Erp/SistemaArea/Create
        public ActionResult Create()
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var area = new SistemaArea { AlteradoPor = idUsuario };

            return View(area);
        }

        // POST: Erp/SistemaArea/Create
        [HttpPost]
        public ActionResult Create(SistemaArea area)
        {
            try
            {
                area.AlteradoEm = DateTime.Now;
                TryUpdateModel(area);

                if (ModelState.IsValid)
                {
                    service.Gravar(area);
                    return RedirectToAction("Index");
                }

                return View(area);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(area);
            }
        }

        // GET: Erp/SistemaArea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var area = service.Find((int)id);

            if (area == null)
            {
                return HttpNotFound();
            }

            area.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(area);
        }

        // POST: Erp/SistemaArea/Edit/5
        [HttpPost]
        public ActionResult Edit(SistemaArea area)
        {
            try
            {
                area.AlteradoEm = DateTime.Now;
                TryUpdateModel(area);

                if (ModelState.IsValid)
                {
                    service.Gravar(area);
                    return RedirectToAction("Index");
                }

                return View(area);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(area);
            }
        }

        // GET: Erp/SistemaArea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var area = service.Find((int)id);

            if (area == null)
            {
                return HttpNotFound();
            }

            return View(area);
        }

        // POST: Erp/SistemaArea/Delete/5
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
                var area = service.Find(id);
                if (area == null)
                {
                    return HttpNotFound();
                }
                ModelState.AddModelError(string.Empty, e.Message);
                return View(area);
            }
        }
    }
}
