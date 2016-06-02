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
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class SistemaRegraController : Controller
    {
        private IBaseService<SistemaRegra> service;
        private ILogin login;

        public SistemaRegraController(IBaseService<SistemaRegra> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/SistemaRegra
        public ActionResult Index()
        {
            var regras = service.Listar()
                .OrderBy(x => x.Descricao)
                .AsEnumerable();

            return View(regras);
        }

        // GET: Erp/SistemaRegra/Details/5
        public ActionResult Details(int id)
        {
            var regra = service.Find(id);

            if (regra != null)
            {
                return HttpNotFound();
            }

            return View(regra);
        }

        // GET: Erp/SistemaRegra/Create
        public ActionResult Create()
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var regra = new SistemaRegra { AlteradoPor = idUsuario, Observ = string.Empty };

            return View(regra);
        }

        // POST: Erp/SistemaRegra/Create
        [HttpPost]
        public ActionResult Create(SistemaRegra regra)
        {
            try
            {
                regra.AlteradoEm = DateTime.Now;
                regra.Observ = regra.Observ == null ? string.Empty : regra.Observ;
                TryUpdateModel(regra);

                if (ModelState.IsValid)
                {
                    service.Gravar(regra);
                    return RedirectToAction("Index");
                }

                return View(regra);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(regra);
            }
        }

        // GET: Erp/SistemaRegra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var regra = service.Find((int)id);

            if (regra == null)
            {
                return HttpNotFound();
            }

            regra.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(regra);
        }

        // POST: Erp/SistemaRegra/Edit/5
        [HttpPost]
        public ActionResult Edit(SistemaRegra regra)
        {
            try
            {
                regra.AlteradoEm = DateTime.Now;
                regra.Observ = regra.Observ == null ? string.Empty : regra.Observ;
                TryUpdateModel(regra);

                if (ModelState.IsValid)
                {
                    service.Gravar(regra);
                    return RedirectToAction("Index");
                }

                return View(regra);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(regra);
            }
        }

        // GET: Erp/SistemaRegra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var regra = service.Find((int)id);

            if (regra == null)
            {
                return HttpNotFound();
            }

            return View(regra);
        }

        // POST: Erp/SistemaRegra/Delete/5
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
                var regra = service.Find(id);
                if (regra == null)
                {
                    return HttpNotFound();
                }
                ModelState.AddModelError(string.Empty, e.Message);
                return View(regra);
            }
        }
    }
}
