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
    [AreaAuthorizeAttribute("Erp", Roles = "departamento-r")]
    public class DepartamentoController : Controller
    {
        private IBaseService<Departamento> service;
        private ILogin login;

        public DepartamentoController(IBaseService<Departamento> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Departamento
        public ActionResult Index()
        {
            var idEmpresa = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            var departamentos = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();
                
            return View(departamentos);
        }

        // GET: Erp/Departamento/Details/5
        public ActionResult Details(int id)
        {
            var departamento = service.Find(id);

            if (departamento == null)
            {
                return HttpNotFound();
            }

            return View(departamento);
        }

        // GET: Erp/Departamento/Create
        [AreaAuthorizeAttribute("Erp", Roles = "departamento-c")]
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var departamento = new Departamento { AlteradoPor = usuario.Id, IdEmpresa = usuario.IdEmpresa, IdEstado = usuario.Empresa.IdEstado, Cidade = usuario.Empresa.Cidade };

            return PartialView(departamento);
        }

        // POST: Erp/Departamento/Create
        [HttpPost]
        public ActionResult Create(Departamento departamento)
        {
            try
            {
                departamento.AlteradoEm = DateTime.Now;
                TryUpdateModel(departamento);

                if (ModelState.IsValid)
                {
                    service.Gravar(departamento);
                    return Json(new { success = true });
                }

                return PartialView(departamento);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(departamento);
            }
        }

        // GET: Erp/Departamento/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "departamento-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var departamento = service.Find((int)id);

            if (departamento == null)
            {
                return HttpNotFound();
            }

            departamento.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return PartialView(departamento);
        }

        // POST: Erp/Departamento/Edit/5
        [HttpPost]
        public ActionResult Edit(Departamento departamento)
        {
            try
            {
                departamento.AlteradoEm = DateTime.Now;
                TryUpdateModel(departamento);

                if (ModelState.IsValid)
                {
                    service.Gravar(departamento);
                    return Json(new { success = true });
                }

                return PartialView(departamento);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(departamento);
            }
        }

        // GET: Erp/Departamento/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "departamento-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var departamento = service.Find((int)id);

            if (departamento == null)
            {
                return HttpNotFound();
            }

            departamento.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return PartialView(departamento);
        }

        // POST: Erp/Departamento/Delete/5
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
                var departamento = service.Find(id);
                if (departamento == null)
                {
                    return HttpNotFound();
                }
                return PartialView(departamento);
            }
        }
    }
}
