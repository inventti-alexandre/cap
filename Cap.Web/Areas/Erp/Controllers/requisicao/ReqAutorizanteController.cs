using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    [Route("Requisicao-autorizante")]
    public class ReqAutorizanteController : Controller
    {
        private IBaseService<ReqAutorizante> service;
        private ILogin login;

        public ReqAutorizanteController(IBaseService<ReqAutorizante> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/ReqAutorizante
        public ActionResult Index()
        {
            return View(getAutorizantes());
        }

        private IEnumerable<ReqAutorizante> getAutorizantes()
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
            return service.Listar()
                .Where(x => x.EmpresaId == idEmpresa)
                .ToList()
                .OrderBy(x => x.Autorizante.Nome);
        }

        // GET: Erp/ReqAutorizante/Details/5
        public ActionResult Details(int id)
        {
            var autorizante = service.Find(id);

            if (autorizante == null)
            {
                return HttpNotFound();
            }

            return PartialView(autorizante);
        }

        // GET: Erp/ReqAutorizante/Create
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            var item = new ReqAutorizante
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = usuario.Id,
                EmpresaId = usuario.IdEmpresa
            };

            return PartialView(item);
        }

        // POST: Erp/ReqAutorizante/Create
        [HttpPost]
        public ActionResult Create(ReqAutorizante item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true, data = getAutorizantes() });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                return Json(new { error = e.Message });
            }
        }

        // GET: Erp/ReqAutorizante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = service.Find((int)id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // POST: Erp/ReqAutorizante/Edit/5
        [HttpPost]
        public ActionResult Edit(ReqAutorizante item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true, data = getAutorizantes() });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                return Json(new { error = e.Message });
            }
        }

        // GET: Erp/ReqAutorizante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = service.Find((int)id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // POST: Erp/ReqAutorizante/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return Json(new { success = true, data = getAutorizantes() });
            }
            catch (ArgumentException e)
            {
                return Json(new { error = e.Message });
            }
        }
    }
}
