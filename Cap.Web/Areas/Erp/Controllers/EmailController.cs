using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Cap.Web.Common;
using Cap.Domain.Models.Gen;
using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;

namespace Cap.Web.Areas.Erp.Controllers
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class EmailController : Controller
    {
        IBaseService<AgendaEmail> service;
        ILogin login;

        public EmailController(IBaseService<AgendaEmail> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        [AllowAnonymous]
        public ActionResult Emails(int idAgenda, bool crud = false, string nome = "")
        {
            var emails = service.Listar()
                .Where(x => x.IdAgenda == idAgenda)
                .ToList();

            ViewBag.IdAgenda = idAgenda;
            ViewBag.Crud = crud;
            ViewBag.Nome = nome;
            return PartialView(emails);
        }

        // GET: Erp/Emails/Create
        public ActionResult Create(int idAgenda, string nome = "")
        {
            var email = new AgendaEmail { IdAgenda = idAgenda, AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name) };

            ViewBag.Nome = nome;
            return View(email);
        }

        // POST: Erp/Emails/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, string nomePesquisa = "")
        {
            var email = new AgendaEmail
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = Convert.ToInt32(collection["AlteradoPor"]),
                Email = collection["Email"],
                Contato = collection["Contato"],
                IdAgenda = Convert.ToInt32(collection["IdAgenda"]),
            };

            ViewBag.Nome = nomePesquisa;
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(email);
                    return RedirectToAction("Details", "Agenda", new { id = email.IdAgenda, nomePesquisa });
                }

                return View(email);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(email);
            }
        }

        // GET: Erp/Emails/Edit/5
        public ActionResult Edit(int id, int idAgenda, string nome)
        {
            var email = service.Find(id);

            if (email == null)
            {
                return HttpNotFound();
            }

            ViewBag.Nome = nome;
            return View(email);
        }

        // POST: Erp/Emails/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection, string nome = "")
        {
            var email = new AgendaEmail
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = Convert.ToInt32(collection["AlteradoPor"]),
                Email = collection["Email"],
                Contato = collection["Contato"],
                IdAgenda = Convert.ToInt32(collection["IdAgenda"]),
                Id = Convert.ToInt32(collection["Id"])
            };

            ViewBag.Nome = nome;
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(email);
                    return RedirectToAction("Details", "Agenda", new { id = email.IdAgenda, nome = nome });
                }

                return View(email);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(email);
            }
        }

        // GET: Erp/Email/Delete/5
        public ActionResult Delete(int? id, string nome = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var email = service.Find((int)id);

            if (email == null)
            {
                return HttpNotFound();
            }

            ViewBag.Nome = nome;
            return View(email);
        }

        // POST: Erp/Email/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, string nome = "")
        {
            try
            {
                var email = service.Excluir(id);
                return RedirectToAction("Details", "Agenda", new { id = email.IdAgenda, nome = nome });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var email = service.Find(id);
                if (email == null)
                {
                    return HttpNotFound();
                }
                return View(email);
            }
        }
    }
}