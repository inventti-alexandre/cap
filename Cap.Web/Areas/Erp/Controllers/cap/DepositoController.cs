using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Cap;
using Cap.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.cap
{
    [AreaAuthorizeAttribute("Erp", Roles = "deposito-r")]
    public class DepositoController : Controller
    {
        private IBaseService<Deposito> service;
        private IBaseService<Parcela> serviceParcela;
        private ILogin login;

        public DepositoController(IBaseService<Deposito> service, ILogin login, IBaseService<Parcela> serviceParcela)
        {
            this.service = service;
            this.login = login;
            this.serviceParcela = serviceParcela;
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
        [AreaAuthorizeAttribute("Erp", Roles = "deposito-c")]
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
        [AreaAuthorizeAttribute("Erp", Roles = "deposito-u")]
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
        [AreaAuthorizeAttribute("Erp", Roles = "deposito-d")]
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

        // GET: Erp/Deposito/CreateIntoModal
        [AreaAuthorizeAttribute("Erp", Roles = "deposito-c")]
        public ActionResult CreateIntoModal(int idPedido, int numParcelas)
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            // TODO: criar um servico que passando o pedido o sistema retorne o id possivel para deposito
            // se nao houver retorna um novo objeto Deposito
                        
            var deposito = new Deposito { AlteradoPor = usuario.Id, IdEmpresa = usuario.IdEmpresa };

            ViewBag.NumParcelas = numParcelas;
            ViewBag.IdPedido = idPedido;
            return PartialView(deposito);
        }

        // POST: Erp/Deposito/CreateIntoModal
        [AreaAuthorizeAttribute("Erp", Roles = "deposito-c")]
        [HttpPost]
        public ActionResult CreateIntoModal(Deposito deposito, int numParcelas, int idPedido)
        {
            try
            {
                deposito.AlteradoEm = DateTime.Now;
                deposito.Cpf = (deposito.Cpf == null ? string.Empty : deposito.Cpf);
                deposito.Observ = (deposito.Observ == null ? string.Empty : deposito.Observ);
                TryUpdateModel("deposito");

                if (ModelState.IsValid)
                {
                    deposito.Id = service.Gravar(deposito);

                    var parcelas = serviceParcela.Listar()
                        .Where(x => x.IdPedido == idPedido)
                        .OrderByDescending(x => x.Id)
                        .ToList()
                        .Take(numParcelas);

                    if (parcelas.Count() > 0)
                    {
                        foreach (var item in parcelas)
                        {
                            item.IdDeposito = deposito.Id;
                            serviceParcela.Gravar(item);
                        }
                    }
                    return Json(new { success = true });
                }

                ViewBag.NumParcelas = numParcelas;
                ViewBag.IdPedido = idPedido;
                return PartialView(deposito);
            }
            catch (Exception e)
            {
                return PartialView(e.Message);
            }
        }

    }
}
