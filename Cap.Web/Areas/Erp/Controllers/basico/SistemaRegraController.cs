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
    [AreaAuthorizeAttribute("Erp", Roles = "sistemaregra-r")]
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
        [AreaAuthorizeAttribute("Erp", Roles = "sistemaregra-r")]
        public ActionResult Index()
        {
            var regras = service.Listar()
                .OrderBy(x => x.Descricao)
                .AsEnumerable();

            return View(regras);
        }

        // GET: Erp/SistemaRegra/Details/5
        [AreaAuthorizeAttribute("Erp", Roles = "sistemaregra-r")]
        public PartialViewResult Details(int id)
        {
            return PartialView(service.Find(id));
        }

        // GET: Erp/SistemaRegra/Create
        [AreaAuthorizeAttribute("Erp", Roles = "sistemaregra-c")]
        public PartialViewResult Create()
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var regra = new SistemaRegra { AlteradoPor = idUsuario };

            return PartialView(regra);
        }

        // POST: Erp/SistemaRegra/Create
        [HttpPost]
        [AreaAuthorizeAttribute("Erp", Roles = "sistemaregra-c")]
        public ActionResult Create(SistemaRegra regra)
        {
            try
            {
                regra.AlteradoEm = DateTime.Now;
                TryUpdateModel(regra);

                if (ModelState.IsValid)
                {
                    service.Gravar(regra);
                    return Json(new { success = true });
                    //return RedirectToAction("Index");
                }
                
                return PartialView(regra);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(regra);
            }
        }

        // GET: Erp/SistemaRegra/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "sistemaregra-u")]
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
            return PartialView(regra);
        }

        // POST: Erp/SistemaRegra/Edit/5
        [HttpPost]
        [AreaAuthorizeAttribute("Erp", Roles = "sistemaregra-u")]
        public ActionResult Edit(SistemaRegra regra)
        {
            try
            {
                regra.AlteradoEm = DateTime.Now;
                TryUpdateModel(regra);

                if (ModelState.IsValid)
                {
                    service.Gravar(regra);
                    return Json(new { success = true });
                    //return RedirectToAction("Index");
                }

                return PartialView(regra);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(regra);
            }
        }

        // GET: Erp/SistemaRegra/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "sistemaregra-d")]
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

            return PartialView(regra);
        }

        // POST: Erp/SistemaRegra/Delete/5
        [HttpPost]
        [AreaAuthorizeAttribute("Erp", Roles = "sistemaregra-d")]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return Json(new { success = true });
                //return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                var regra = service.Find(id);
                if (regra == null)
                {
                    return HttpNotFound();
                }
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(regra);
            }
        }
    }
}
