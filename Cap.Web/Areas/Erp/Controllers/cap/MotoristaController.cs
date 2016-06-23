using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [Common.AreaAuthorizeAttribute("Erp", Roles = "motorista-r")]
    public class MotoristaController : Controller
    {
        private IBaseService<Motorista> service;
        private ILogin login;

        public MotoristaController(IBaseService<Motorista> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Motorista
        public ActionResult Index()
        {
            var motoristas = service.Listar().ToList()
                .OrderBy(x => x.Usuario.Nome);

            return View(motoristas);
        }

        // GET: Erp/Motorista/Details/5
        public ActionResult Details(int id)
        {
            var motorista = service.Find(id);

            if (motorista == null)
            {
                return HttpNotFound();
            }

            return PartialView(motorista);
        }

        // GET: Erp/Motorista/Create
        [Common.AreaAuthorizeAttribute("Erp", Roles = "motorista-c")]
        public ActionResult Create()
        {            
            return View(new Motorista());
        }

        // POST: Erp/Motorista/Create
        [HttpPost]
        public ActionResult Create(Motorista motorista)
        {
            try
            {
                motorista.AlteradoEm = DateTime.Now;
                TryUpdateModel(motorista);

                if (ModelState.IsValid)
                {
                    service.Gravar(motorista);
                    return Json(new { success = true });
                }

                return PartialView(motorista);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(motorista);
            }
        }

        // GET: Erp/Motorista/Edit/5
        [Common.AreaAuthorizeAttribute("Erp", Roles = "motorista-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var motorista = service.Find((int)id);

            if (motorista == null)
            {
                return HttpNotFound();
            }

            return PartialView(motorista);
        }

        // POST: Erp/Motorista/Edit/5
        [HttpPost]
        public ActionResult Edit(Motorista motorista)
        {
            try
            {
                motorista.AlteradoEm = DateTime.Now;
                TryUpdateModel(motorista);

                if (ModelState.IsValid)
                {
                    service.Gravar(motorista);
                    return Json(new { success = true });
                }

                return PartialView(motorista);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(motorista);
            }
        }

        // GET: Erp/Motorista/Delete/5
        [Common.AreaAuthorizeAttribute("Erp", Roles = "motorista-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var motorista = service.Find((int)id);

            if (motorista == null)
            {
                return HttpNotFound();
            }

            return PartialView(motorista);
        }

        // POST: Erp/Motorista/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return Json(new { success = true });
            }
            catch (ArgumentException e)
            {
                var motorista = service.Find(id);

                if (motorista == null)
                {
                    return HttpNotFound();
                }

                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(motorista);
            }
        }
    }
}
