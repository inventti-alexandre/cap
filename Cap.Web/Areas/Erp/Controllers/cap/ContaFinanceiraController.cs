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
    [AreaAuthorizeAttribute("Erp", Roles = "contafinanceira-r")]
    public class ContaFinanceiraController : Controller
    {
        private IBaseService<ContaFinanceira> service;
        private ILogin login;

        public ContaFinanceiraController(IBaseService<ContaFinanceira> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/ContaFinanceira
        public ActionResult Index(int id)
        {
            var contas = service.Listar()
                .Where(x => x.IdGrupoFinanceiro == id)
                .ToList()
                .OrderBy(x => x.GrupoFinanceiro.Descricao)
                .ThenBy(x => x.Descricao)
                .AsEnumerable();

            ViewBag.IdGrupoFinanceiro = id;
            return View(contas);
        }

        // GET: Erp/ContaFinanceira/Details/5
        public ActionResult Details(int id)
        {
            var conta = service.Find(id);

            if (conta == null)
            {
                return HttpNotFound();
            }

            return View(conta);
        }

        // GET: Erp/ContaFinanceira/Create
        [AreaAuthorizeAttribute("Erp", Roles = "contafinanceira-c")]
        public ActionResult Create(int idGrupoFinanceiro)
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var conta = new ContaFinanceira { IdEmpresa = usuario.IdEmpresa, AlteradoPor = usuario.Id, IdGrupoFinanceiro = idGrupoFinanceiro, TipoConta = TipoConta.Debito, Contabiliza = true };

            return View(conta);
        }

        // POST: Erp/ContaFinanceira/Create
        [HttpPost]
        public ActionResult Create(ContaFinanceira conta)
        {
            try
            {
                conta.AlteradoEm = DateTime.Now;
                TryUpdateModel(conta);

                if (ModelState.IsValid)
                {
                    service.Gravar(conta);
                    return RedirectToAction("Index", new { id = conta.IdGrupoFinanceiro });
                }

                return View(conta);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(conta);
            }
        }

        // GET: Erp/ContaFinanceira/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "contafinanceira-u")]
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

            conta.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(conta);
        }

        // POST: Erp/ContaFinanceira/Edit/5
        [HttpPost]
        public ActionResult Edit(ContaFinanceira conta)
        {
            try
            {
                conta.AlteradoEm = DateTime.Now;
                TryUpdateModel(conta);

                if (ModelState.IsValid)
                {
                    service.Gravar(conta);
                    return RedirectToAction("Index", new { id = conta.IdGrupoFinanceiro });
                }

                return View(conta);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(conta);
            }
        }

        // GET: Erp/ContaFinanceira/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "contafinanceira-d")]
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

        // POST: Erp/ContaFinanceira/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var conta = service.Excluir(id);
                return RedirectToAction("Index", new { id = conta.IdGrupoFinanceiro });
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
