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
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class DepositoController : Controller
    {
        private IBaseService<Deposito> service;
        private ILogin login;

        public DepositoController(IBaseService<Deposito> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Deposito
        public ActionResult Index(string pesquisa = "")
        {
            ViewBag.Pesquisa = pesquisa;
            return View();
        }

        public PartialViewResult Depositos(string pesquisa = "")
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var depositos = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa && x.Favorecido.Contains(pesquisa))
                .OrderBy(x => x.Favorecido)
                .ToList();

            ViewBag.Pesquisa = pesquisa;
            return PartialView(depositos);
        }

        // GET: Erp/Deposito/Details/5
        public ActionResult Details(int id, string pesquisa = "")
        {
            var deposito = service.Find(id);

            if (deposito == null)
            {
                return HttpNotFound();
            }

            ViewBag.Pesquisa = pesquisa;
            return View(deposito);
        }

        // GET: Erp/Deposito/Create
        public ActionResult Create(string pesquisa = "")
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var deposito = new Deposito { AlteradoPor = usuario.Id, IdEmpresa = usuario.IdEmpresa };

            ViewBag.Pesquisa = pesquisa;
            return View(deposito);
        }

        // POST: Erp/Deposito/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdEmpresa,Favorecido,IdBanco,Agencia,Conta,Cpf,Observ,Poupanca,AlteradoPor")] Deposito deposito)
        {
            try
            {
                deposito.AlteradoEm = DateTime.Now;
                TryUpdateModel(deposito);

                if (ModelState.IsValid)
                {
                    service.Gravar(deposito);
                    return RedirectToAction("Index", new { pesquisa = deposito.Favorecido });
                }

                ViewBag.Pesquisa = deposito.Favorecido;
                return View(deposito);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Pesquisa = deposito.Favorecido;
                return View(deposito);
            }
        }

        // GET: Erp/Deposito/Edit/5
        public ActionResult Edit(int? id, string pesquisa)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deposito = service.Find((int)id);

            if (deposito == null)
            {
                return HttpNotFound();
            }

            ViewBag.Pesquisa = deposito.Favorecido;
            return View(deposito);
        }

        // POST: Erp/Deposito/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Ativo,IdEmpresa,Favorecido,IdBanco,Agencia,Conta,Cpf,Observ,Poupanca,AlteradoPor")] Deposito deposito)
        {
            try
            {
                deposito.AlteradoEm = DateTime.Now;
                TryUpdateModel(deposito);

                if (ModelState.IsValid)
                {
                    service.Gravar(deposito);
                    return RedirectToAction("Index", new { pesquisa = deposito.Favorecido });
                }

                ViewBag.Pesquisa = deposito.Favorecido;
                return View(deposito);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Pesquisa = deposito.Favorecido;
                return View(deposito);
            }
        }

        // GET: Erp/Deposito/Delete/5
        public ActionResult Delete(int? id, string pesquisa = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deposito = service.Find((int)id);

            if (deposito == null)
            {
                return HttpNotFound();
            }

            ViewBag.Pesquisa = deposito.Favorecido;
            return View(deposito);
        }

        // POST: Erp/Deposito/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, string pesquisa = "")
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index", new { pesquisa = pesquisa });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var deposito = service.Find(id);
                if (deposito == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Pesquisa = deposito.Favorecido;
                return View(deposito);
            }
        }
    }
}
