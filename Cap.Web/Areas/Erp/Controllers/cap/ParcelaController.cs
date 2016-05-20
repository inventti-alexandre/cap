using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class ParcelaController : Controller
    {
        IBaseService<Parcela> service;
        ILogin login;

        public ParcelaController(IBaseService<Parcela> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Parcela
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Parcelas(int idPedido, bool soAtivos = true)
        {
            var parcelas = service.Listar()
                .Where(x => x.IdPedido == idPedido
                        && (soAtivos == false || (soAtivos == true && x.Ativo == true)))
                .ToList();

            return PartialView(parcelas);
        }

        // GET: Erp/Parcela/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Erp/Parcela/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Erp/Parcela/Create
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

        // GET: Erp/Parcela/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Erp/Parcela/Edit/5
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

        // GET: Erp/Parcela/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Erp/Parcela/Delete/5
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
