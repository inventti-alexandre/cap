using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    public class LogisticaController : Controller
    {

        private IBaseService<Logistica> service;
        private ILogin login;
        private ILogistica serviceConclusao;

        public LogisticaController(IBaseService<Logistica> service, ILogin login, ILogistica serviceConclusao)
        {
            this.service = service;
            this.login = login;
            this.serviceConclusao = serviceConclusao;
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
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            var logistica = new Logistica { DataServico = DateTime.Today.Date, EmpresaId = usuario.IdEmpresa, UsuarioId = usuario.Id };

            return PartialView(logistica);
        }

        // POST: Erp/Logistica/Create
        [HttpPost]
        public ActionResult Create(Logistica item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }

        // GET: Erp/Logistica/Edit/5
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

            return PartialView(item);
        }

        // POST: Erp/Logistica/Edit/5
        [HttpPost]
        public ActionResult Edit(Logistica item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }

        // GET: Erp/Logistica/Delete/5
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

        // POST: Erp/Logistica/Delete/5
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
                ModelState.AddModelError(string.Empty, e.Message);

                var item = service.Find(id);

                if (item == null)
                {
                    return HttpNotFound();
                }

                return PartialView(item);
            }
        }

        // GET: Erp/Logistica/Concluir/5
        public ActionResult ConcluirServico(int id)
        {
            var item = service.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            item.ConcluidoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return PartialView(item);
        }

        // POST: Erp/Logistica/Concluir/{logistica}
        [HttpPost]
        public ActionResult ConcluirServico(Logistica logistica)
        {
            try
            {
                serviceConclusao.Concluir(logistica);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(logistica);
            }
        }

        // GET: Erp/Logistica/CancelarConclusao/5
        public ActionResult CancelarConclusao(int? id)
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

        // POST: Erp/Logistica/CancelarConclusao/5
        [HttpPost]
        public ActionResult CancelarConclusao(int id)
        {
            try
            {
                serviceConclusao.CancelarConclusao(id);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var item = service.Find(id);
                if (item == null)
                {
                    return HttpNotFound();
                }

                return PartialView(item);
            }
        }
    }
}
