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
    [AreaAuthorizeAttribute("Erp", Roles = "contatipo-r")]
    public class ContaTipoController : Controller
    {
        IBaseService<ContaTipo> service;
        ILogin login;

        public ContaTipoController(IBaseService<ContaTipo> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/ContaTipo
        public ActionResult Index()
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var tipos = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(tipos);
        }

        // GET: Erp/ContaTipo/Details/5
        public ActionResult Details(int id)
        {
            var tipo = service.Find(id);

            if (tipo == null)
            {
                return HttpNotFound();
            }

            return View(tipo);
        }

        // GET: Erp/ContaTipo/Create
        [AreaAuthorizeAttribute("Erp", Roles = "contatipo-c")]
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            var tipo = new ContaTipo
            {
                AlteradoPor = usuario.Id,
                IdEmpresa = usuario.IdEmpresa
            };

            return View(tipo);
        }

        // POST: Erp/ContaTipo/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdEmpresa,Descricao,AlteradoPor")] ContaTipo tipo)
        {
            try
            {
                tipo.AlteradoEm = DateTime.Now;
                TryUpdateModel(tipo);

                if (ModelState.IsValid)
                {
                    service.Gravar(tipo);
                    return RedirectToAction("Index");
                }

                return View(tipo);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(tipo);
            }
        }

        // GET: Erp/ContaTipo/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "contatipo-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipo = service.Find((int)id);

            if (tipo == null)
            {
                return HttpNotFound();
            }

            tipo.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(tipo);
        }

        // POST: Erp/ContaTipo/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,IdEmpresa,Descricao,AlteradoPor")] ContaTipo tipo)
        {
            try
            {
                tipo.AlteradoEm = DateTime.Now;
                TryUpdateModel(tipo);

                if (ModelState.IsValid)
                {
                    service.Gravar(tipo);
                    return RedirectToAction("Index");
                }

                return View(tipo);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(tipo);
            }
        }

        // GET: Erp/ContaTipo/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "contatipo-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipo = service.Find((int)id);

            if (tipo == null)
            {
                return HttpNotFound();
            }

            return View(tipo);
        }

        // POST: Erp/ContaTipo/Delete/5
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
                var tipo = service.Find(id);
                if (tipo == null)
                {
                    return HttpNotFound();
                }
                return View(tipo);
            }
        }
    }
}
