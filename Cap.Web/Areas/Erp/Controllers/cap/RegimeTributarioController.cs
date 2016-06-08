using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Cap.Web.Common;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "regimetributario-r")]
    public class RegimeTributarioController : Controller
    {
        private IBaseService<RegimeTributario> service;
        private ILogin login;

        public RegimeTributarioController(IBaseService<RegimeTributario> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/RegimeTributario
        public ActionResult Index()
        {
            var regimes = service.Listar()
                .OrderBy(x => x.Descricao)
                .AsEnumerable();

            return View(regimes);
        }

        // GET: Erp/RegimeTributario/Details/5
        public ActionResult Details(int id)
        {
            var regime = service.Find(id);

            if (regime == null)
            {
                return HttpNotFound();
            }

            return View(regime);
        }

        // GET: Erp/RegimeTributario/Create
        [AreaAuthorizeAttribute("Erp", Roles = "regimetributario-c")]
        public ActionResult Create()
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            return View(new RegimeTributario { AlteradoPor = idUsuario });
        }

        // POST: Erp/RegimeTributario/Create
        [HttpPost]
        public ActionResult Create(RegimeTributario regime)
        {
            try
            {
                regime.AlteradoEm = DateTime.Now;
                TryUpdateModel(regime);

                if (ModelState.IsValid)
                {
                    service.Gravar(regime);
                    return RedirectToAction("Index");
                }

                return View(regime);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(regime);
            }
        }

        // GET: Erp/RegimeTributario/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "regimetributario-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var regime = service.Find((int)id);

            if (regime == null)
            {
                return HttpNotFound();
            }

            return View(regime);
        }

        // POST: Erp/RegimeTributario/Edit/5
        [HttpPost]
        public ActionResult Edit(RegimeTributario regime)
        {
            try
            {
                regime.AlteradoEm = DateTime.Now;
                TryUpdateModel(regime);

                if (ModelState.IsValid)
                {
                    service.Gravar(regime);
                    return RedirectToAction("Index");
                }

                return View(regime);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(regime);
            }
        }

        // GET: Erp/RegimeTributario/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "regimetributario-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var regime = service.Find((int)id);

            if (regime == null)
            {
                return HttpNotFound();
            }

            return View(regime);
        }

        // POST: Erp/RegimeTributario/Delete/5
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
                var regime = service.Find(id);
                if (regime == null)
                {
                    return HttpNotFound();
                }
                ModelState.AddModelError(string.Empty, e.Message);
                return View(regime);
            }
        }
    }
}
