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
    [AreaAuthorizeAttribute("Erp", Roles = "usuario-r")]
    public class UsuarioController : Controller
    {
        IBaseService<Usuario> service;
        ILogin login;

        public UsuarioController(IBaseService<Usuario> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Usuario
        [AreaAuthorizeAttribute("Erp", Roles = "usuario-r")]
        public ActionResult Index()
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
            var usuarios = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Nome).ToList();

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
        [AreaAuthorizeAttribute("Erp", Roles = "usuario-c")]
        public ActionResult Create()
        {
            return View(new Usuario() { IdEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa });
        }

        // POST: Erp/Usuario/Create
        [HttpPost]
        [AreaAuthorizeAttribute("Erp", Roles = "usuario-c")]
        public ActionResult Create([Bind(Include ="Nome,Email,Senha,Telefone,Ramal,Roles,IdEmpresa")] Usuario usuario)
        {
            try
            {
                usuario.IdEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
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
        [AreaAuthorizeAttribute("Erp", Roles = "usuario-u")]
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
        [AreaAuthorizeAttribute("Erp", Roles = "usuario-u")]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,Telefone,Ramal,Roles,Ativo,CadastradoEm,ExcluidoEm,Senha,IdEmpresa")] Usuario usuario)
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
        [AreaAuthorizeAttribute("Erp", Roles = "usuario-d")]
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
        [AreaAuthorizeAttribute("Erp", Roles = "usuario-d")]
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
