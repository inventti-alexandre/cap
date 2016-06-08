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
    [AreaAuthorizeAttribute("Erp", Roles = "moeda-r")]
    public class MoedaController : Controller
    {
        private IBaseService<Moeda> service;
        private ILogin login;

        public MoedaController(IBaseService<Moeda> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Moeda
        public ActionResult Index()
        {
            var idEmpresa = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            var moedas = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .ToList();

            return View(moedas);
        }

        // GET: Erp/Moeda/Details/5
        public ActionResult Details(int id)
        {
            var moeda = service.Find(id);

            if (moeda == null)
            {
                return HttpNotFound();
            }

            return View(moeda);
        }

        // GET: Erp/Moeda/Create
        [AreaAuthorizeAttribute("Erp", Roles = "moeda-c")]
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            Moeda moeda = new Moeda { AlteradoPor = usuario.Id, IdEmpresa = usuario.IdEmpresa };

            return View(moeda);
        }

        // POST: Erp/Moeda/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="IdEmpresa,Descricao,AlteradoPor")] Moeda moeda)
        {
            try
            {
                moeda.AlteradoEm = DateTime.Now;
                TryUpdateModel(moeda);

                if (ModelState.IsValid)
                {
                    service.Gravar(moeda);
                    return RedirectToAction("Index");
                }

                return View(moeda);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
        }

        // GET: Erp/Moeda/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "moeda-u")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var moeda = service.Find((int)id);

            if (moeda == null)
            {
                return HttpNotFound();
            }

            moeda.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(moeda);
        }

        // POST: Erp/Moeda/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdEmpresa,Descricao,AlteradoPor,Id,Padrao")] Moeda moeda)
        {
            try
            {
                moeda.AlteradoEm = DateTime.Now;
                TryUpdateModel(moeda);

                if (ModelState.IsValid)
                {
                    service.Gravar(moeda);
                    return RedirectToAction("Index");
                }

                return View(moeda);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(moeda);
            }
        }

        // GET: Erp/Moeda/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "moeda-d")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var moeda = service.Find((int)id);

            if (moeda == null)
            {
                return HttpNotFound();
            }

            moeda.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(moeda);
        }

        // POST: Erp/Moeda/Delete/5
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
                var moeda = service.Find(id);
                if (moeda == null)
                {
                    return HttpNotFound();
                }
                return View(moeda);
            }
        }
    }
}
