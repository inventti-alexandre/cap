using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.basico
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class UsuarioController : Controller
    {
        IBaseService<Usuario> service;

        public UsuarioController(IBaseService<Usuario> service)
        {
            this.service = service;
        }

        // GET: Erp/Usuario
        public ActionResult Index()
        {
            var usuarios = service.Listar().OrderBy(x => x.Nome).ToList();

            return View(usuarios);
        }

        // GET: Erp/Usuario/Details/5
        public ActionResult Details(int id)
        {
            var usuario = service.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // GET: Erp/Usuario/Create
        public ActionResult Create()
        {
            return View(new Usuario());
        }

        // POST: Erp/Usuario/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Nome,Email,Login,Senha,Telefone,Ramal,Roles")] Usuario usuario)
        {
            try
            {
                usuario.CadastradoEm = DateTime.Now;
                usuario.Ramal = usuario.Ramal == null ? string.Empty : usuario.Ramal;
                TryUpdateModel(usuario);

                if (ModelState.IsValid)
                {
                    service.Gravar(usuario);
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }

        // GET: Erp/Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Erp/Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,Login,Telefone,Ramal,Roles,Ativo,CadastradoEm,ExcluidoEm,Senha")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(usuario);
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(usuario);
            }
        }

        // GET: Erp/Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = service.Find((int)id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Erp/Usuario/Delete/5
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
                var usuario = service.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
        }
    }
}
