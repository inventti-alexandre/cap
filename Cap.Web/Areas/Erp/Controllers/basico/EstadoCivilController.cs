using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Cap.Web.Areas.Erp.Controllers.basico
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class EstadoCivilController : Controller
    {
        private IBaseService<EstadoCivil> service;
        private ILogin login;

        public EstadoCivilController(IBaseService<EstadoCivil> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/EstadoCivil
        public ActionResult Index()
        {
            var estados = service.Listar()
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(estados);
        }

        // GET: Erp/EstadoCivil/Details/5
        public ActionResult Details(int? id)
        {
            var estado = service.Find((int)id);

            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // GET: Erp/EstadoCivil/Create
        public ActionResult Create()
        {
            return View(new EstadoCivil() { Ativo = true });
        }

        // POST: Erp/EstadoCivil/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Descricao")] EstadoCivil estado)
        {
            try
            {
                estado.AlteradoEm = DateTime.Now;
                estado.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(estado);

                if (ModelState.IsValid)
                {
                    service.Gravar(estado);
                    return RedirectToAction("Index");
                }

                return View(estado);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(estado);
            }
        }

        // GET: Erp/EstadoCivil/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estado = service.Find((int)id);

            if (estado == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // POST: Erp/EstadoCivil/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,Descricao,Ativo")] EstadoCivil estado)
        {
            try
            {
                estado.AlteradoEm = DateTime.Now;
                estado.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(estado);

                if (ModelState.IsValid)
                {
                    service.Gravar(estado);
                    return RedirectToAction("Index");
                }

                return View(estado);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(estado);
            }
        }

        // GET: Erp/EstadoCivil/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estado = service.Find((int)id);

            if (id == null)
            {
                return HttpNotFound();
            }

            return View(estado);
        }

        // POST: Erp/EstadoCivil/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var estado = service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var estado = service.Find(id);
                if (estado == null)
                {
                    return HttpNotFound();
                }
                return View(estado);
            }
        }
    }
}
