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
            var itens = service.Listar()
                .Where(x => x.IdReqRequisicao == id)
                .OrderBy(x => x.Id)
                .AsEnumerable();

            ViewBag.IdRequisicao = id;
            return PartialView(itens);
        }

        // GET: Erp/ReqMaterial/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Erp/ReqMaterial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Erp/ReqMaterial/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
    }
}
