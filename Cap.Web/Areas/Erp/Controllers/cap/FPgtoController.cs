using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class FPgtoController : Controller
    {
        IBaseService<FPgto> service;
        ILogin login;

        public FPgtoController(IBaseService<FPgto> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/FPgto
        public ActionResult Index()
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
            var formas = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(formas);
        }

        // GET: Erp/FPgto/Details/5
        public ActionResult Details(int id)
        {
            var forma = service.Find(id);

            if (forma == null)
            {
                return HttpNotFound();
            }

            return View(forma);
        }

        // GET: Erp/FPgto/Create
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(new FPgto() { IdEmpresa = usuario.IdEmpresa, AlteradoPor = usuario.Id });
        }

        // POST: Erp/FPgto/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Descricao,IdEmpresa,AlteradoPor")] FPgto forma)
        {
            try
            {
                forma.AlteradoEm = DateTime.Now;
                TryUpdateModel(forma);

                if (ModelState.IsValid)
                {
                    service.Gravar(forma);
                    return RedirectToAction("Index");
                }

                return View(forma);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(forma);
            }
        }

        // GET: Erp/FPgto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forma = service.Find((int)id);

            if (forma == null)
            {
                return HttpNotFound();
            }

            forma.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(forma);
        }

        // POST: Erp/FPgto/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Descricao,Ativo,IdEmpresa,AlteradoPor")] FPgto forma)
        {
            try
            {
                forma.AlteradoEm = DateTime.Now;
                TryUpdateModel(forma);

                if (ModelState.IsValid)
                {
                    service.Gravar(forma);
                    return RedirectToAction("Index");
                }

                return View(forma);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(forma);
            }
        }

        // GET: Erp/FPgto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forma = service.Find((int)id);

            if (forma == null)
            {
                return HttpNotFound();
            }

            return View(forma);
        }

        // POST: Erp/FPgto/Delete/5
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
                var forma = service.Find(id);
                if (forma == null)
                {
                    return HttpNotFound();
                }
                return View(forma);
            }
        }
    }
}
