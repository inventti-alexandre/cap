using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Cap.Web.Common;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "admin")]
    public class ContaController : Controller
    {
        IBaseService<Conta> service;
        ILogin login;

        public ContaController(IBaseService<Conta> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Conta
        public ActionResult Index()
        {
            var idEmpresa = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var contas = service.Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            return View(contas);
        }

        // GET: Erp/Conta/Details/5
        public ActionResult Details(int id)
        {
            var conta = service.Find(id);

            if (conta == null)
            {
                return HttpNotFound();
            }

            return View(conta);
        }

        // GET: Erp/Conta/Create
        public ActionResult Create()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            var conta = new Conta
            {
                AlteradoPor = usuario.Id,
                IdEmpresa = usuario.IdEmpresa,
                ChequeAtual = 1,
                DataSaldo = DateTime.Today.Date,
                DataSaldoAnterior = DateTime.Today.Date.AddDays(-1),
                Saldo = 0,
                SaldoAnterior = 0
            };

            return View(conta);
        }

        // POST: Erp/Conta/Create
        [HttpPost]
        public ActionResult Create(Conta conta)
        {
            try
            {
                conta.AlteradoEm = DateTime.Now;
                TryUpdateModel(conta);

                if (ModelState.IsValid)
                {
                    service.Gravar(conta);
                    return RedirectToAction("Index");
                }

                return View(conta);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(conta);
            }
        }

        // GET: Erp/Conta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var conta = service.Find((int)id);

            if (conta == null)
            {
                return HttpNotFound();
            }

            return View(conta);
        }

        // POST: Erp/Conta/Edit/5
        [HttpPost]
        public ActionResult Edit(Conta conta)
        {
            try
            {
                conta.AlteradoEm = DateTime.Now;
                TryUpdateModel(conta);

                if (ModelState.IsValid)
                {
                    service.Gravar(conta);
                    return RedirectToAction("Index");
                }

                return View(conta);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(conta);
            }
        }

        // GET: Erp/Conta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var conta = service.Find((int)id);

            if (conta == null)
            {
                return HttpNotFound();
            }

            return View(conta);
        }

        // POST: Erp/Conta/Delete/5
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
                var conta = service.Find(id);
                if (conta == null)
                {
                    return HttpNotFound();
                }
                return View(conta);
            }
        }
    }
}
