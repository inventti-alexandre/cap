using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "pedido-r")]
    public class PedidoController : Controller
    {
        private IBaseService<Pedido> service;
        private IBaseService<GrupoCusto> serviceGrupoCusto;
        private IBaseService<CentroCusto> serviceCentroCusto;
        private ILogin login;
        
        public PedidoController(IBaseService<Pedido> service, IBaseService<GrupoCusto> serviceGrupoCusto, IBaseService<CentroCusto> serviceCentroCusto, ILogin login)
        {
            this.service = service;
            this.serviceGrupoCusto = serviceGrupoCusto;
            this.serviceCentroCusto = serviceCentroCusto;
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
        [AreaAuthorizeAttribute("Erp", Roles = "pedido-c")]
        public ActionResult Create(string message = "")
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var idGrupoCusto = serviceGrupoCusto.Listar().Where(x => x.Ativo == true && x.IdEmpresa == usuario.IdEmpresa).OrderBy(x => x.Descricao).First().Id;
            var idCentroCusto = 0;

            var pedido = new Pedido { AlteradoPor = usuario.Id, IdEmpresa = usuario.IdEmpresa, CriadoPor = usuario.Id, DataNF = DateTime.Today.Date, IdGrupoCusto = idGrupoCusto, IdCentroCusto = idCentroCusto };

            ViewBag.Message = message;
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
        [AreaAuthorizeAttribute("Erp", Roles = "pedido-u")]
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
        [AreaAuthorizeAttribute("Erp", Roles = "pedido-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pedido = service.Find((int)id);

            if (pedido == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdPedido = id;
            return PartialView();
        }

        // POST: Erp/Pedido/Delete/5
        [HttpPost]
        [AreaAuthorizeAttribute("Erp", Roles = "pedido-d")]
        public JsonResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                //return RedirectToAction("Index");
                return Json(new { success = true });
            }
            catch (ArgumentException e)
            {
                return Json(new { success = false, message = e.ToString() });
            }
        }
    }
}
