using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;
using System.Net;
using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;
using Cap.Domain.Abstract.Admin;

namespace Cap.Web.Areas.Erp.Controllers
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class TelefoneController : Controller
    {
        IBaseService<AgendaTelefone> service;
        ILogin login;

        public TelefoneController(IBaseService<AgendaTelefone> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Telefone
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Telefones(int idAgenda)
        {
            var telefones = service.Listar()
                .Where(x => x.IdAgenda == idAgenda)
                .ToList();

            ViewBag.IdAgenda = idAgenda;
            return PartialView(telefones);
        }

        // GET: Erp/Telefone/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Erp/Telefone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Erp/Telefone/Create
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

        // GET: Erp/Telefone/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Erp/Telefone/Edit/5
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

        // GET: Erp/Telefone/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Erp/Telefone/Delete/5
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
