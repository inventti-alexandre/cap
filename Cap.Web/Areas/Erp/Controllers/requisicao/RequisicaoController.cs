using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Requisicao;
using Cap.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: Erp/Requisicao/Details/5
        public ActionResult Details(int id)
        {
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
        public ActionResult Editar(int id)
        {
            try
            {
                ReqRequisicao requisicao = service.Find(id);

                if (requisicao == null)
                {
                    return HttpNotFound();
                }

                return View(requisicao);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ReqRequisicao requisicao = service.Find(id);

                if (requisicao == null)
                {
                    return HttpNotFound();
                }

                return View(requisicao);
            }
        }

        // POST: Erp/Requisicao/Editar/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Erp/Requisicao/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Erp/Requisicao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
