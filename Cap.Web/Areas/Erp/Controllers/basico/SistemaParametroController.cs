using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.basico
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class SistemaParametroController : Controller
    {
        private IBaseService<SistemaParametro> service;
        private ILogin login;

        public SistemaParametroController(IBaseService<SistemaParametro> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/SistemaParametro
        public ActionResult Index()
        {
            var parametros = service.Listar().OrderBy(x => x.Codigo).ToList();

            return View(parametros);
        }

        // GET: Erp/SistemaParametro/Details/5
        public ActionResult Details(int id)
        {
            var parametro = service.Find(id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        // GET: Erp/SistemaParametro/Create
        public ActionResult Create()
        {
            return View(new SistemaParametro());
        }

        // POST: Erp/SistemaParametro/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Codigo,Valor,Descricao")] SistemaParametro parametro)
        {
            try
            {
                parametro.AlteradoEm = DateTime.Now;
                parametro.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(parametro);

                if (ModelState.IsValid)
                {
                    service.Gravar(parametro);
                    return RedirectToAction("Index");
                }

                return View(parametro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(parametro);
            }
        }

        // GET: Erp/SistemaParametro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parametro = service.Find((int)id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        // POST: Erp/SistemaParametro/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Valor,Descricao,Ativo")] SistemaParametro parametro)
        {
            try
            {
                parametro.AlteradoEm = DateTime.Now;
                parametro.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                TryUpdateModel(parametro);

                if (ModelState.IsValid)
                {
                    service.Gravar(parametro);
                    return RedirectToAction("Index");
                }

                return View(parametro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(parametro);
            }
        }

        // GET: Erp/SistemaParametro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var parametro = service.Find((int)id);

            if (parametro == null)
            {
                return HttpNotFound();
            }

            return View(parametro);
        }

        // POST: Erp/SistemaParametro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var parametro = service.Find(id);
                if (parametro == null)
                {
                    return HttpNotFound();
                }
                return View(parametro);
            }
        }
    }
}
