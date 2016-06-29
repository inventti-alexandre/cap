﻿using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Requisicao;
using Cap.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    [AreaAuthorizeAttribute("Erp", Roles = "requisicao-r")]
    public class RequisicaoController : Controller
    {
        IBaseService<ReqRequisicao> service;
        ILogin login;

        public RequisicaoController(IBaseService<ReqRequisicao> service, ILogin login)
        {
            this.service = service;
            this.login = login;
        }

        // GET: Erp/Requisicao
        public ActionResult Index()
        {
            // TODO: painel de controle das requisicoes
            return View();
        }

        // GET: Erp/Requisicao/Create
        public ActionResult Nova()
        {
            var item = new ReqRequisicao { CotarAte = DateTime.Today.Date.AddDays(1), EntregarDia = DateTime.Today.Date.AddDays(2), SolicitadoEm = DateTime.Today.Date, Situacao = Situacao.EmDigitacao };

            return View(item);
        }

        // POST: Erp/Requisicao/Create
        [HttpPost]
        public ActionResult Nova(ReqRequisicao requisicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(requisicao);
                    return RedirectToAction("Editar", new { id = requisicao.Id });
                }

                return View(requisicao);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(requisicao);
            }
        }

        // GET: Erp/Requisicao/Editar/5
        public ActionResult Editar(int? id)
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

            return View(item);
        }

        // POST: Erp/Requisicao/Editar/5
        [HttpPost]
        public ActionResult Editar(ReqRequisicao item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    ViewBag.Message = "Requisição gravada";
                    return View(item);
                }

                string modelErrors = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

                if (modelErrors.Count() > 0)
                {
                    ViewBag.Message = modelErrors;
                }
                
                return View(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                string modelErrors = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

                ViewBag.Message = modelErrors;
                return View(e);
            }
        }

        // GET: Erp/Requisicao/Delete/5
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

        // POST: Erp/Requisicao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);
                return Json(new { success = true, id = id });
            }
            catch (ArgumentException e)
            {
                var item = service.Find(id);

                if (item == null)
                {
                    return HttpNotFound();
                }

                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(e);
            }
        }

        // GET: Erp/Requisicao/FinishEdit/5
        public ActionResult FinishEdit(int? id)
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

            return PartialView(id);
        }

        // GET: Erp/Requisicao/Send/5
        public ActionResult PreSend(int id)
        {
            var item = service.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            item.Situacao = Situacao.Cotar;
            service.Gravar(item);

            return RedirectToActionPermanent("Send", new { id = id });
        }

        public ActionResult Send(int id)
        {
            return View(service.Find(id));
        }

        // GET: Erp/Requisicao/Imprimir/5
        public ActionResult Imprimir(int id)
        {
            var item = service.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            // TODO: imprimir requisicao
            return PartialView(item);
        }

        // GET: Erp/Requisicao/ImprimirRequisicao/5
        public ActionResult ImprimirRequisicao(int id, int idFornecedor)
        {
            var item = service.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

    }
}
