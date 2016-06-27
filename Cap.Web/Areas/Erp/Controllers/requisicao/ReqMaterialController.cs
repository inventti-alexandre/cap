using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{   
    public class ReqMaterialController : Controller
    {

        private IBaseService<ReqMaterial> service;
        private ILogin login;

        public ReqMaterialController(IBaseService<ReqMaterial> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/ReqMaterial/5
        public ActionResult Index(int id)
        {
            var itens = getItens(id);

            ViewBag.IdRequisicao = id;
            return PartialView(itens);
        }

        // GET: Erp/ReqMaterial/Details/5
        public ActionResult Details(int id)
        {
            var item = service.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // GET: Erp/ReqMaterial/Create
        public ActionResult Create(int id)
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            var item = new ReqMaterial { AlteradoPor = usuario.Id, IdReqRequisicao = id };

            return PartialView(item);
        }

        // POST: Erp/ReqMaterial/Create
        [HttpPost]
        public ActionResult Create(ReqMaterial item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true, id = item.IdReqRequisicao });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }

        // GET: Erp/ReqMaterial/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Erp/ReqMaterial/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Erp/ReqMaterial/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Erp/ReqMaterial/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private List<ReqMaterial> getItens(int idReqRequisicao)
        {
            return service.Listar()
                .Where(x => x.IdReqRequisicao == idReqRequisicao)
                .OrderBy(x => x.Id)
                .ToList();
        }
    }
}
