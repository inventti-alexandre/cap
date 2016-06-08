using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.basico
{
    [AreaAuthorizeAttribute("Erp", Roles = "estado-r")]
    public class EstadoController : Controller
    {
        private IBaseService<Estado> service;

        public EstadoController(IBaseService<Estado> service)
        {
            this.service = service;
        }

        // GET: Erp/Estado
        public ActionResult Index()
        {
            var estados = service.Listar()
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(estados);
        }

        // GET: Erp/Estado/Details/5
        public ActionResult Details(int id)
        {
            var estado = service.Find(id);

            if (estado == null)
            {
                return HttpNotFound();
            }


            return View(estado);
        }

        // GET: Erp/Estado/Create
        [AreaAuthorizeAttribute("Erp", Roles = "estado-c")]
        public ActionResult Create()
        {
            return View(new Estado() { Ativo = true });
        }

        // POST: Erp/Estado/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Descricao,UF")] Estado estado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(estado);
                    return RedirectToAction("Index");
                }
                return View(estado);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(estado);
            }
        }

        // GET: Erp/Estado/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "estado-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estado = service.Find((int)id);

            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // POST: Erp/Estado/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Descricao,UF,Ativo")] Estado estado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(estado);
                    return RedirectToAction("Index");
                }
                return View(estado);                
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(estado);
            }
        }

        // GET: Erp/Estado/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "estado-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estado = service.Find((int)id);

            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // POST: Erp/Estado/Delete/5
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
                var estado = service.Find(id);
                if (estado == null)
                {
                    return HttpNotFound();
                }
                return View(estado);
            }
        }
    }
}
