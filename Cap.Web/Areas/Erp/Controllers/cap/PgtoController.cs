using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class PgtoController : Controller
    {
        private IBaseService<Pgto> service;
        private ILogin login;

        public PgtoController(IBaseService<Pgto> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Pgto
        public ActionResult Index()
        {
            var pgtos = service.Listar().OrderBy(x => x.Descricao).ToList();

            return View(pgtos);
        }

        // GET: Erp/Pgto/Details/5
        public ActionResult Details(int id)
        {
            var pgto = service.Find(id);

            if (pgto == null)
            {
                return HttpNotFound();
            }

            return View(pgto);
        }

        // GET: Erp/Pgto/Create
        public ActionResult Create()
        {
            return View(new Pgto());
        }

        // POST: Erp/Pgto/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Descricao,Imposto")] Pgto pgto)
        {
            try
            {
                pgto.AlteradoEm = DateTime.Now;
                pgto.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(pgto);

                if (ModelState.IsValid)
                {
                    service.Gravar(pgto);
                    return RedirectToAction("Index");
                }

                return View(pgto);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(pgto);
            }
        }

        // GET: Erp/Pgto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pgto = service.Find((int)id);

            if (pgto == null)
            {
                return HttpNotFound();
            }

            return View(pgto);
        }

        // POST: Erp/Pgto/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Descricao,Imposto,Ativo")] Pgto pgto)
        {
            try
            {
                pgto.AlteradoEm = DateTime.Now;
                pgto.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(pgto);

                if (ModelState.IsValid)
                {
                    service.Gravar(pgto);
                    return RedirectToAction("Index");
                }

                return View(pgto);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(pgto);
            }
        }

        // GET: Erp/Pgto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pgto = service.Find((int)id);

            if (pgto == null)
            {
                return HttpNotFound();
            }

            return View(pgto);
        }

        // POST: Erp/Pgto/Delete/5
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
                var pgto = service.Find(id);
                if (pgto == null)
                {
                    return HttpNotFound();
                }
                return View(pgto);
            }
        }
    }
}
