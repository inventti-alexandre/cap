using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class FornecedorController : Controller
    {
        private IBaseService<Fornecedor> service;
        private IBaseService<Agenda> serviceAgenda;
        private ILogin login;

        public FornecedorController(IBaseService<Fornecedor> service, IBaseService<Agenda> serviceAgenda, ILogin login)
        {
            this.service = service;
            this.serviceAgenda = serviceAgenda;
            this.login = login;
        }

        // GET: Erp/Fornecedor
        public ActionResult Index(string pesquisa = "")
        {
            ViewBag.Pesquisa = pesquisa;
            return View();
        }

        public PartialViewResult Fornecedores(string pesquisa = "")
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;
            pesquisa = pesquisa.ToUpper().Trim();

            if (string.IsNullOrEmpty(pesquisa))
            {
                return PartialView(new List<Fornecedor>());
            }

            var fornecedores = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa && x.Fantasia.Contains(pesquisa))
                .OrderBy(x => x.Fantasia)
                .ToList();
                
            ViewBag.Pesquisa = pesquisa;
            return PartialView(fornecedores);
        }

        // GET: Erp/Fornecedor/Details/5
        public ActionResult Details(int id, string pesquisa = "")
        {
            var fornecedor = service.Find(id);

            if (fornecedor == null)
            {
                return HttpNotFound();
            }

            ViewBag.Pesquisa = pesquisa;
            return View(fornecedor);
        }

        // GET: Erp/Fornecedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Erp/Fornecedor/Create
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

        // GET: Erp/Fornecedor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Erp/Fornecedor/Edit/5
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

        // GET: Erp/Fornecedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Erp/Fornecedor/Delete/5
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
