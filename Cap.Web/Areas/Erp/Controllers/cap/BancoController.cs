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
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class BancoController : Controller
    {
        private IBaseService<Banco> service;
        private ILogin login;

        public BancoController(IBaseService<Banco> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Banco
        public ActionResult Index()
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
            var bancos = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(bancos);
        }

        // GET: Erp/Banco/Details/5
        public ActionResult Details(int id)
        {
            var banco = service.Find(id);

            if (banco == null)
            {
                return HttpNotFound();
            }

            return View(banco);
        }

        // GET: Erp/Banco/Create
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(new Banco() { IdEmpresa = usuario.IdEmpresa, AlteradoPor = usuario.Id });
        }

        // POST: Erp/Banco/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Descricao,Razao,NumFebraban,AlteradoPor,IdEmpresa")] Banco banco)
        {
            try
            {
                banco.AlteradoEm = DateTime.Now;
                TryUpdateModel(banco);

                if (ModelState.IsValid)
                {
                    service.Gravar(banco);
                    return RedirectToAction("Index");
                }

                return View(banco);                
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(banco);
            }
        }

        // GET: Erp/Banco/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var banco = service.Find((int)id);

            if (banco == null)
            {
                return HttpNotFound();
            }

            banco.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(banco);
        }

        // POST: Erp/Banco/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Descricao,Razao,NumFebraban,IdEmpresa,AlteradoPor")] Banco banco)
        {
            try
            {
                var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                banco.AlteradoEm = DateTime.Now;
                banco.AlteradoPor = usuario.Id;
                banco.IdEmpresa = usuario.IdEmpresa;
                TryUpdateModel(banco);

                if (ModelState.IsValid)
                {
                    service.Gravar(banco);
                    return RedirectToAction("Index");
                }

                return View(banco);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(banco);
            }
        }

        // GET: Erp/Banco/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var banco = service.Find((int)id);

            if (banco == null)
            {
                return HttpNotFound();
            }

            return View(banco);
        }

        // POST: Erp/Banco/Delete/5
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
                var banco = service.Find(id);
                if (banco == null)
                {
                    return HttpNotFound();
                }
                return View(banco);
            }
        }
    }
}
