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
    public class SistemaTelaController : Controller
    {
        private IBaseService<SistemaTela> service;
        private ILogin login;

        public SistemaTelaController(IBaseService<SistemaTela> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/SistemaTela
        public ActionResult Index()
        {
            var telas = service.Listar()
                .OrderBy(x => x.Descricao)
                .AsEnumerable();

            return View(telas);
        }

        // GET: Erp/SistemaTela/Details/5
        public ActionResult Details(int id)
        {
            var tela = service.Find(id);

            if (tela == null)
            {
                return HttpNotFound();
            }

            return View(tela);
        }

        // GET: Erp/SistemaTela/Create
        public ActionResult Create()
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var tela = new SistemaTela { AlteradoPor = idUsuario };

            return View(tela);
        }

        // POST: Erp/SistemaTela/Create
        [HttpPost]
        public ActionResult Create(SistemaTela tela)
        {
            try
            {
                tela.AlteradoEm = DateTime.Now;
                TryUpdateModel(tela);

                if (ModelState.IsValid)
                {
                    service.Gravar(tela);
                    return RedirectToAction("Index");
                }

                return View(tela);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(tela);
            }
        }

        // GET: Erp/SistemaTela/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tela = service.Find((int)id);

            if (tela == null)
            {
                return HttpNotFound();
            }

            return View(tela);
        }

        // POST: Erp/SistemaTela/Edit/5
        [HttpPost]
        public ActionResult Edit(SistemaTela tela)
        {
            try
            {
                tela.AlteradoEm = DateTime.Now;
                TryUpdateModel(tela);

                if (ModelState.IsValid)
                {
                    service.Gravar(tela);
                    return RedirectToAction("Index");
                }

                return View(tela);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(tela);
            }
        }

        // GET: Erp/SistemaTela/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tela = service.Find((int)id);

            if (tela == null)
            {
                return HttpNotFound();
            }

            return View(tela);
        }

        // POST: Erp/SistemaTela/Delete/5
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
                var tela = service.Find(id);

                if (tela == null)
                {
                    return HttpNotFound();
                }

                ModelState.AddModelError(string.Empty, e.Message);
                return View(tela);
            }
        }
    }
}
