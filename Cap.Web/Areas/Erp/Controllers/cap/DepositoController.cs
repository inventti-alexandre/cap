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
        public ActionResult CreateIntoModal(int idPedido, int numParcelas, int idParcela = 0)
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            // TODO: criar um servico que passando o pedido o sistema retorne o id possivel para deposito
            // se nao houver retorna um novo objeto Deposito

            var deposito = new Deposito { AlteradoPor = usuario.Id, IdEmpresa = usuario.IdEmpresa, Observ = string.Empty, Cpf = string.Empty };

            ViewBag.NumParcelas = numParcelas;
            ViewBag.IdPedido = idPedido;
            ViewBag.IdParcela = idParcela;
            return PartialView(deposito);
        }

        // POST: Erp/Deposito/CreateIntoModal
        [AreaAuthorizeAttribute("Erp", Roles = "deposito-c")]
        [HttpPost]
        public ActionResult CreateIntoModal(Deposito deposito, int numParcelas, int idPedido, int idParcela)
        {
            try
            {
                deposito.AlteradoEm = DateTime.Now;
                deposito.Cpf = (deposito.Cpf == null ? string.Empty : deposito.Cpf);
                deposito.Observ = (deposito.Observ == null ? string.Empty : deposito.Observ);
                TryUpdateModel(deposito);

                if (ModelState.IsValid)
                {
                    if (numParcelas != 0) 
                    {
                        // origem: inclusao de parcelas

                        var parcelas = serviceParcela.Listar()
                            .Where(x => x.IdPedido == idPedido)
                            .OrderByDescending(x => x.Id)
                            .ToList()
                            .Take(numParcelas);

                        if (parcelas.Count() > 0)
                        {
                            foreach (var item in parcelas)
                            {
                                var idDep = service.Gravar(new Deposito
                                {
                                    Agencia = deposito.Agencia,
                                    AlteradoEm = deposito.AlteradoEm,
                                    AlteradoPor = deposito.AlteradoPor,
                                    Ativo = deposito.Ativo,
                                    Conta = deposito.Conta,
                                    Cpf = deposito.Cpf,
                                    Favorecido = deposito.Favorecido,
                                    IdBanco = deposito.IdBanco,
                                    IdEmpresa = deposito.IdEmpresa,
                                    Observ = deposito.Observ,
                                    Poupanca = deposito.Poupanca
                                });

                                item.IdDeposito = idDep;
                                serviceParcela.Gravar(item);
                            }
                        }
                        return Json(new { success = true, idPedido = idPedido });
                    }
                    else
                    {
                        // origem: definicao das informacoes da parcela
                        var parcela = serviceParcela.Find(idParcela);

                        if (parcela == null)
                        {
                            ViewBag.Message = "Parcela inexistente";
                        }
                        else
                        {
                            parcela.IdDeposito = service.Gravar(deposito);
                            serviceParcela.Gravar(parcela);
                            return Json(new { success = true, idPedido = parcela.IdPedido });
                        }
                    }
                    
                }

                ViewBag.NumParcelas = numParcelas;
                ViewBag.IdPedido = idPedido;
                return PartialView(deposito);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                ViewBag.NumParcelas = numParcelas;
                ViewBag.IdPedido = idPedido;
                return PartialView(deposito);
            }
        }

        // GET: Erp/Deposito/EditIntoModal
        [AreaAuthorizeAttribute("Erp", Roles = "deposito-u")]
        public ActionResult EditIntoModal(int idParcela)
        {
            var parcela = serviceParcela.Find(idParcela);

            if (parcela == null)
            {
                return Json(new { success = false, error = "Parcela inexistente" });
            }

            if (parcela.IdDeposito == null)
            {
                return Json(new { success = false, error = "Nenhum depósito atribuído a esta parcela" });
            }

            var deposito = service.Find((int)parcela.IdDeposito);

            if (deposito == null)
            {
                return Json(new { success = false, error = "Depósito inexistente" });
            }

            deposito.AlteradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            ViewBag.IdPedido = parcela.IdPedido;
            ViewBag.IdParcela = parcela.Id;
            return PartialView(deposito);
        }

        // POST: Erp/Deposito/EditIntoModal
        [AreaAuthorizeAttribute("Erp", Roles = "deposito-u")]
        [HttpPost]
        public ActionResult EditIntoModal(Deposito deposito, int idParcela, int idPedido)
        {
            try
            {
                deposito.AlteradoEm = DateTime.Now;
                deposito.Cpf = (deposito.Cpf == null ? string.Empty : deposito.Cpf);
                deposito.Observ = (deposito.Observ == null ? string.Empty : deposito.Observ);
                TryUpdateModel(deposito);

                if (ModelState.IsValid)
                {
                    service.Gravar(deposito);
                    return Json(new { success = true });
                }

                ViewBag.IdPedido = idPedido;
                ViewBag.IdParcela = idParcela;
                return PartialView(deposito);
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        // POST: Erp/Deposito/CancelDepIntoModal
        [AreaAuthorizeAttribute("Erp", Roles = "deposito-d")]
        public JsonResult CancelDepIntoModal(int idParcela)
        {
            try
            {
                // TODO: quando puder levar para Domain.Service
                var parcela = serviceParcela.Find(idParcela);

                if (parcela == null)
                {
                    throw new ArgumentException("Parcela inexistente");
                }

                if (parcela.IdDeposito == null)
                {
                    // nenhuma acao necessaria
                    return Json(new { success = true });
                }

                if (parcela.Pago == true)
                {
                    return Json(new { error = "Esta parcela já foi paga" });
                }

                service.Excluir((int)parcela.IdDeposito);
                parcela.IdDeposito = null;
                serviceParcela.Gravar(parcela);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        // GET: Erp/Deposito/InfoIntoModal
        public ActionResult InfoIntoModal(int idParcela)
        {
            var deposito = service.Find((int)serviceParcela.Find(idParcela).IdDeposito);

            return PartialView(deposito);
        }

    }
}
