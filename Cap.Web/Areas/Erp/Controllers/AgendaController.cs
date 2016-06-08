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
    [AreaAuthorizeAttribute("Erp", Roles = "agenda-r")]
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

                ViewBag.Nome = nome;
                return PartialView(contatos);
            }

            return PartialView(new List<Agenda>());
        }

        // GET: Erp/Agenda/Details/5
        public ActionResult Details(int id, string nome = "")
        {
            var contato = service.Find(id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            ViewBag.Nome = nome;
            return View(contato);
        }

        // GET: Erp/Agenda/Create
        [AreaAuthorizeAttribute("Erp", Roles = "agenda-c")]
        public ActionResult Create(string nome = "")
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            ViewBag.NomePesquisa = nome;
            return View(new Agenda() { IdEmpresa = usuario.IdEmpresa, AlteradoPor = usuario.Id });
        }

        // POST: Erp/Agenda/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Agenda contato = new Agenda
            {
                Nome = collection["Nome"],
                Contato = collection["Contato"],
                Endereco = collection["Endereco"],
                Bairro = collection["Bairro"],
                Cidade = collection["Cidade"],
                IdEstado = Convert.ToInt32(collection["IdEstado"]),
                Cep = collection["Cep"],
                AlteradoEm = DateTime.Now,
                WebSite = collection["WebSite"],
                Observ = collection["Observ"],
                AlteradoPor = Convert.ToInt32(collection["AlteradoPor"]),
                IdEmpresa = Convert.ToInt32(collection["IdEmpresa"])
            };
            try
            {
                if (ModelState.IsValid)
                {
                    contato.Id = service.Gravar(contato);
                    return RedirectToAction("Create", "Telefone", new { idAgenda = contato.Id, nome = contato.Nome });
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
        [AreaAuthorizeAttribute("Erp", Roles = "agenda-u")]
        public ActionResult Edit(int? id, string nome = "")
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
            ViewBag.Nome = nome;
            return View(contato);
        }

        // POST: Erp/Agenda/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection, string nomePesquisa)
        {
            Agenda contato = new Agenda
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = Convert.ToInt32(collection["AlteradoPor"]),
                Ativo = collection["Ativo"].ToString() == "false" ? false : true,
                Bairro = collection["Bairro"].ToString(),
                Cep = collection["Cep"].ToString(),
                Cidade = collection["Cidade"].ToString(),
                Contato = collection["Contato"].ToString(),
                Endereco = collection["Endereco"].ToString(),
                Id = Convert.ToInt32(collection["Id"]),
                IdEmpresa = Convert.ToInt32(collection["IdEmpresa"]),
                IdEstado = Convert.ToInt32(collection["IdEstado"]),
                Nome = collection["Nome"],
                Observ = collection["Observ"],
                WebSite = collection["WebSite"]
            };

            try
            {

                ViewBag.Nome = nomePesquisa;

                if (ModelState.IsValid)
                {
                    service.Gravar(contato);
                    return RedirectToAction("Details", new { id = contato.Id, nome = nomePesquisa });
                }
                return View(contato);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Nome = nomePesquisa;
                return View(contato);
            }
        }

        // GET: Erp/Agenda/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "agenda-d")]
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
