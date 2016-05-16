using Cap.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;
using Cap.Domain.Abstract.Admin;

namespace Cap.Web.Areas.Erp.Controllers
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class AgendaController : Controller
    {
        IBaseService<Agenda> service;
        ILogin login;

        public AgendaController(IBaseService<Agenda> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Agenda
        public ActionResult Index(string nome = "")
        {
            ViewBag.Nome = nome;
            return View();
        }

        public PartialViewResult Contatos(string nome = "")
        {
            if (!string.IsNullOrEmpty(nome))
            {
                var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
                var contatos = service.Listar()
                    .Where(x => x.Nome.Contains(nome) && x.IdEmpresa == idEmpresa)
                    .ToList();
                return PartialView(contatos);
            }

            return PartialView(new List<Agenda>());
        }

        // GET: Erp/Agenda/Details/5
        public ActionResult Details(int id)
        {
            var contato = service.Find(id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            return View(contato);
        }

        // GET: Erp/Agenda/Create
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(new Agenda() { IdEmpresa = usuario.IdEmpresa, AlteradoPor = usuario.Id });
        }

        // POST: Erp/Agenda/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Nome,Contato,Endereco,Bairro,Cidade,IdEstado,Cep,WebSite,Observ,AlteradoPor,IdEmpresa")]Agenda contato)
        {
            try
            {
                contato.AlteradoEm = DateTime.Now;
                TryUpdateModel(contato);

                if (ModelState.IsValid)
                {
                    service.Gravar(contato);
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(contato);
            }
        }

        // GET: Erp/Agenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contato = service.Find((int)id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            contato.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(contato);
        }

        // POST: Erp/Agenda/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Nome,Contato,Endereco,Bairro,Cidade,IdEstado,Cep,WebSite,Observ,AlteradoPor,IdEmpresa")]Agenda contato)
        {
            try
            {
                contato.AlteradoEm = DateTime.Now;

                if (ModelState.IsValid)
                {
                    service.Gravar(contato);
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(contato);
            }
        }

        // GET: Erp/Agenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contato = service.Find((int)id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            return View(contato);
        }

        // POST: Erp/Agenda/Delete/5
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
                var contato = service.Find(id);
                if (contato == null)
                {
                    return HttpNotFound();
                }
                return View(contato);
            }
        }
    }
}
