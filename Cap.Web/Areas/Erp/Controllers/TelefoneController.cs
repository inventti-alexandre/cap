using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;
using System.Net;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;
using Cap.Domain.Abstract.Admin;

namespace Cap.Web.Areas.Erp.Controllers
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class TelefoneController : Controller
    {
        IBaseService<AgendaTelefone> service;
        ILogin login;

        public TelefoneController(IBaseService<AgendaTelefone> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        public PartialViewResult Telefones(int idAgenda, bool crud = false, string nome = "")
        {
            var telefones = service.Listar()
                .Where(x => x.IdAgenda == idAgenda)
                .ToList();

            ViewBag.IdAgenda = idAgenda;
            ViewBag.Crud = crud;
            ViewBag.Nome = nome;
            return PartialView(telefones);
        }

        // GET: Erp/Telefone/Create
        public ActionResult Create(int idAgenda, string nome = "")
        {
            var telefone = new AgendaTelefone { IdAgenda = idAgenda, AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name) };

            ViewBag.Nome = nome;
            return View(telefone);
        }

        // POST: Erp/Telefone/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdAgenda,Numero,Contato,AlteradoPor")]AgendaTelefone telefone, string nomePesquisa = "")
        {
            ViewBag.Nome = nomePesquisa;
            try
            {
                telefone.AlteradoEm = DateTime.Now;
                TryUpdateModel(telefone);

                if (ModelState.IsValid)
                {
                    service.Gravar(telefone);
                    return RedirectToAction("Details", "Agenda", new { id = telefone.IdAgenda, nomePesquisa });
                }

                return View(telefone);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(telefone);
            }
        }

        // GET: Erp/Telefone/Edit/5
        public ActionResult Edit(int id, int idAgenda, string nome = "")
        {
            var telefone = service.Find(id);

            if (telefone == null)
            {
                return HttpNotFound();
            }

            ViewBag.Nome = nome;
            return View(telefone);
        }

        // POST: Erp/Telefone/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,IdAgenda,Numero,Contato,Ativo,AlteradoPor")] AgendaTelefone telefone, string nome)
        {
            ViewBag.Nome = nome;
            try
            {
                telefone.AlteradoEm = DateTime.Now;
                TryUpdateModel(telefone);

                if (ModelState.IsValid)
                {
                    service.Gravar(telefone);
                    return RedirectToAction("Details", "Agenda", new { id = telefone.IdAgenda, nome = nome });
                }

                return View(telefone);
            } 
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(telefone);
            }
        }

        // GET: Erp/Telefone/Delete/5
        public ActionResult Delete(int? id, string nome = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var telefone = service.Find((int)id);

            if (telefone == null)
            {
                return HttpNotFound();
            }

            ViewBag.Nome = nome;
            return View(telefone);
        }

        // POST: Erp/Telefone/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, string nome = "")
        {
            try
            {
                var telefone = service.Excluir(id);
                return RedirectToAction("Details", "Agenda", new { id = telefone.IdAgenda, nome = nome });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var telefone = service.Find(id);
                if (telefone == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Nome = nome;
                return View(telefone);
            }
        }
    }
}
