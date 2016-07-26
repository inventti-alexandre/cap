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
    //[AreaAuthorizeAttribute("Erp", Roles = "sistemarela-r")]
    public class SistemaTelaController : Controller
    {
        private IBaseService<SistemaTela> service;
        private ILogin login;

        public SistemaTelaController(IBaseService<SistemaTela> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/SistemaTela
        //[AreaAuthorizeAttribute("Erp", Roles = "sistemarela-r")]
        public ActionResult Index()
        {
            var telas = service.Listar()
                .OrderBy(x => x.Link)
                .AsEnumerable();

            return View(telas);
        }

        // GET: Erp/SistemaTela/Details/5
        //[AreaAuthorizeAttribute("Erp", Roles = "sistemarela-r")]
        public PartialViewResult Details(int id)
        {            
            return PartialView(service.Find(id));
        }

        // GET: Erp/SistemaTela/Create
        //[AreaAuthorizeAttribute("Erp", Roles = "sistemarela-c")]
        public ActionResult Create()
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var tela = new SistemaTela { AlteradoPor = idUsuario };

            return PartialView(tela);
        }

        // POST: Erp/SistemaTela/Create
        [HttpPost]
        //[AreaAuthorizeAttribute("Erp", Roles = "sistemarela-c")]
        public ActionResult Create(SistemaTela tela)
        {
            try
            {
                tela.AlteradoEm = DateTime.Now;
                TryUpdateModel(tela);

                if (ModelState.IsValid)
                {
                    service.Gravar(tela);
                    return Json(new { success = true });
                }

                return PartialView(tela);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(tela);
            }
        }

        // GET: Erp/SistemaTela/Edit/5
        //[AreaAuthorizeAttribute("Erp", Roles = "sistemarela-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tela = service.Find((int)id);

            if (tela == null)
            {
                return HttpNotFound();
            }

            return PartialView(tela);
        }

        // POST: Erp/SistemaTela/Edit/5
        [HttpPost]
        //[AreaAuthorizeAttribute("Erp", Roles = "sistemarela-u")]
        public ActionResult Edit(SistemaTela tela)
        {
            try
            {
                tela.AlteradoEm = DateTime.Now;
                TryUpdateModel(tela);

                if (ModelState.IsValid)
                {
                    service.Gravar(tela);
                    return Json(new { success = true });
                }

                return PartialView(tela);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(tela);
            }
        }

        // GET: Erp/SistemaTela/Delete/5
        //[AreaAuthorizeAttribute("Erp", Roles = "sistemarela-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tela = service.Find((int)id);

            if (tela == null)
            {
                return HttpNotFound();
            }

            return PartialView(tela);
        }

        // POST: Erp/SistemaTela/Delete/5
        [HttpPost]
        //[AreaAuthorizeAttribute("Erp", Roles = "sistemarela-d")]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return Json(new { success = true });
            }
            catch (ArgumentException e)
            {
                var tela = service.Find(id);

                if (tela == null)
                {
                    return HttpNotFound();
                }

                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(tela);
            }
        }
    }
}
