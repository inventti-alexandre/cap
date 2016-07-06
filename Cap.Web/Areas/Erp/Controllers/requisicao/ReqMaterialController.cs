using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [ChildActionOnly]
        public ActionResult MaterialRequisicao(int id)
        {
            return PartialView(getItens(id));
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

            item.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return PartialView(item);
        }

        // POST: Erp/ReqMaterial/Edit/5
        [HttpPost]
        public ActionResult Edit(ReqMaterial item)
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

        // GET: Erp/ReqMaterial/Delete/5
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

        // POST: Erp/ReqMaterial/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = service.Excluir(id);
                return Json(new { success = true, id = item.IdReqRequisicao });
            }
            catch (ArgumentException e)
            {
                var item = service.Find(id);

                if (item == null)
                {
                    return HttpNotFound();
                }

                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(e);
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
