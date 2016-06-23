using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Abstract.Admin;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class LogisticaController : Controller
    {

        private IBaseService<Logistica> service;
        private ILogin login;

        public LogisticaController(IBaseService<Logistica> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Logistica
        public ActionResult Index()
        {
            ViewBag.Date = DateTime.Now.ToShortDateString();
            return View();
        }

        // GET: Erp/Logistica/GetLogiticaDia/23-06-2016
        public ActionResult GetLogisticaDia(DateTime? dataServico, int idMotorista = 0)
        {

            try
            {
                if (dataServico == null)
                {
                    dataServico = DateTime.Today.Date;
                }

                var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

                var logisticas = service.Listar()
                    .Where(x => x.EmpresaId == idEmpresa
                            && x.DataServico == dataServico
                            && (idMotorista == 0 || x.MotoristaId == idMotorista))
                    .ToList()
                    .OrderBy(x => x.Motorista.Usuario.Nome)
                    .ThenBy(x => x.Id);

                return PartialView(logisticas);
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        // GET: Erp/Logistica/Details/5
        public ActionResult Details(int id)
        {
            var item = service.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // GET: Erp/Logistica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Erp/Logistica/Create
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

        // GET: Erp/Logistica/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Erp/Logistica/Edit/5
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

        // GET: Erp/Logistica/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Erp/Logistica/Delete/5
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
