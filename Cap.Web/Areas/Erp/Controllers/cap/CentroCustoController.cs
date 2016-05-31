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
    public class CentroCustoController : Controller
    {
        private IBaseService<CentroCusto> service;
        private ILogin login;

        public CentroCustoController(IBaseService<CentroCusto> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/CentroCusto
        public ActionResult Index(int id)
        {
            var centros = service.Listar()
                .Where(x => x.IdGrupoCusto == id)
                .ToList()
                .OrderBy(x => x.GrupoCusto.Descricao)
                .ThenBy(x => x.Descricao)
                .AsEnumerable();

            ViewBag.IdGrupoCusto = id;
            return View(centros);
        }

        // GET: Erp/CentroCusto/Details/5
        public ActionResult Details(int id)
        {
            var centro = service.Find(id);

            if (centro == null)
            {
                return HttpNotFound();
            }

            return View(centro);
        }

        // GET: Erp/CentroCusto/Create
        public ActionResult Create(int idGrupoCusto)
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var centro = new CentroCusto {  IdEmpresa = usuario.IdEmpresa, AlteradoPor = usuario.Id, IdGrupoCusto = idGrupoCusto };
            return View(centro);
        }

        // POST: Erp/CentroCusto/Create
        [HttpPost]
        public ActionResult Create(CentroCusto centro)
        {
            try
            {
                centro.AlteradoEm = DateTime.Now;
                TryUpdateModel(centro);

                if (ModelState.IsValid)
                {
                    service.Gravar(centro);
                    return RedirectToAction("Index", new { id = centro.IdGrupoCusto });
                }

                return View(centro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(centro);
            }
        }

        // GET: Erp/CentroCusto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var centro = service.Find((int)id);

            if (id == null)
            {
                return HttpNotFound();
            }

            centro.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(centro);
        }

        // POST: Erp/CentroCusto/Edit/5
        [HttpPost]
        public ActionResult Edit(CentroCusto centro)
        {
            try
            {
                centro.AlteradoEm = DateTime.Now;
                TryUpdateModel(centro);

                if (ModelState.IsValid)
                {
                    service.Gravar(centro);
                    return RedirectToAction("Index", new { id = centro.IdGrupoCusto });
                }

                return View(centro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(centro);
            }
        }

        // GET: Erp/CentroCusto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var centro = service.Find((int)id);

            if (id == null)
            {
                return HttpNotFound();
            }

            return View(centro);
        }

        // POST: Erp/CentroCusto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var centro = service.Excluir(id);
                return RedirectToAction("Index", new { id = centro.IdGrupoCusto });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var centro = service.Find(id);
                if (centro == null)
                {
                    return HttpNotFound();
                }
                return View(centro);
            }
        }
    }
}
