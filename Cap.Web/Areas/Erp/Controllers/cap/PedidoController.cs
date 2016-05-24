using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cap.Web.Common;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class PedidoController : Controller
    {
        private IBaseService<Pedido> service;
        private ILogin login;
        
        public PedidoController(IBaseService<Pedido> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Pedido
        public ActionResult Index()
        {
            return View();
        }

        // GET: Erp/Pedido/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Erp/Pedido/Create
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var pedido = new Pedido { AlteradoPor = usuario.Id, IdEmpresa = usuario.IdEmpresa, CriadoPor = usuario.Id, DataNF = DateTime.Today.Date };

            return View(pedido);
        }

        // POST: Erp/Pedido/Create
        [HttpPost]
        public ActionResult Create(Pedido pedido)
        {
            try
            {
                pedido.CriadoEm = DateTime.Now;
                pedido.AlteradoEm = DateTime.Now;
                TryUpdateModel(pedido);

                if (ModelState.IsValid)
                {
                    pedido.Id = service.Gravar(pedido);
                    return RedirectToAction("Edit", new { id = pedido.Id });
                }

                return View(pedido);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(pedido);
            }
        }

        // GET: Erp/Pedido/Edit/5
        public ActionResult Edit(int id)
        {
            var pedido = service.Find(id);

            if (pedido == null || (pedido.IdEmpresa != login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa))
            {
                return HttpNotFound();
            }

            ViewBag.Message = string.Empty;
            return View(pedido);
        }

        // POST: Erp/Pedido/Edit/5
        [HttpPost]
        public ActionResult Edit(Pedido pedido)
        {
            try
            {
                pedido.AlteradoEm = DateTime.Now;
                TryUpdateModel(pedido);

                if (ModelState.IsValid)
                {
                    ViewBag.Message = "Pedido gravado";
                    service.Gravar(pedido);
                    return View(pedido);
                }

                ViewBag.Message = string.Empty;
                return View(pedido);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(pedido);
            }
        }

        // GET: Erp/Pedido/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Erp/Pedido/Delete/5
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
