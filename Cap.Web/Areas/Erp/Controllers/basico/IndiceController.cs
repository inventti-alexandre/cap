using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Gen;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.basico
{
    [AreaAuthorizeAttribute("Erp", Roles = "indice-r")]
    public class IndiceController : Controller
    {
        private IBaseService<Indice> service;
        private ILogin login;

        public IndiceController(IBaseService<Indice> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Indice
        public ActionResult Index()
        {
            return View(service.Listar()
                .OrderBy(x => x.Descricao)
                .AsEnumerable());
        }

        // GET: Erp/Indice/Details/5
        public PartialViewResult Details(int id)
        {
            return PartialView(service.Find(id));
        }

        // GET: Erp/Indice/Create
        [AreaAuthorizeAttribute("Erp", Roles = "indice-c")]
        public ActionResult Create()
        {
            return PartialView(new Indice { AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name) });
        }

        // POST: Erp/Indice/Create
        [HttpPost]
        public ActionResult Create(Indice indice)
        {
            try
            {
                indice.AlteradoEm = DateTime.Now;
                TryUpdateModel(indice);

                if (ModelState.IsValid)
                {
                    service.Gravar(indice);
                    return Json(new { success = true });
                }

                return PartialView(indice);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(indice);
            }
        }

        // GET: Erp/Indice/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "indice-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var indice = service.Find((int)id);

            if (indice == null)
            {
                return HttpNotFound();
            }

            return PartialView(indice);
        }

        // POST: Erp/Indice/Edit/5
        [HttpPost]
        public ActionResult Edit(Indice indice)
        {
            try
            {
                indice.AlteradoEm = DateTime.Now;
                TryUpdateModel(indice);

                if (ModelState.IsValid)
                {
                    service.Gravar(indice);
                    return Json(new { success = true });
                }

                return PartialView(indice);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(indice);
            }
        }

        // GET: Erp/Indice/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "indice-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var indice = service.Find((int)id);

            if (indice == null)
            {
                return HttpNotFound();
            }

            return PartialView(indice);
        }

        // POST: Erp/Indice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return Json(new { success = true });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var indice = service.Find(id);
                if (indice == null)
                {
                    return HttpNotFound();
                }
                return PartialView(indice);
            }
        }
    }
}
