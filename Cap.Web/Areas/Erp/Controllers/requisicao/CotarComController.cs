﻿using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Requisicao;
using Cap.Web.Areas.Erp.Models;
using Cap.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    [AreaAuthorizeAttribute("Erp", Roles = "cotarcom-r")]
    public class CotarComController : Controller
    {
        IBaseService<CotFornecedor> service;
        IBaseService<ReqRequisicao> serviceRequisicao;
        IBaseService<Fornecedor> serviceFornecedor;
        ICotadoCom serviceCotadoCom;
        ILogin login;

        public CotarComController(IBaseService<CotFornecedor> service, ICotadoCom serviceCotadoCom, IBaseService<ReqRequisicao> serviceRequisicao, IBaseService<Fornecedor> serviceFornecedor, ILogin login)
        {
            this.service = service;
            this.serviceCotadoCom = serviceCotadoCom;
            this.serviceRequisicao = serviceRequisicao;
            this.serviceFornecedor = serviceFornecedor;
            this.login = login;
        }

        // GET: Erp/CotarCom
        public ActionResult Index(int id = 0)
        {
            ViewBag.IdCotGrupo = id;
            return View();
        }

        public ActionResult Fornecedores(int id)
        {
            var fornecedores = service.Listar()
                .Where(x => x.CotGrupoId == id)
                .OrderBy(x => x.Fornecedor.Fantasia)
                .ToList();

            ViewBag.Id = id;
            return PartialView(fornecedores);
        }

        // GET: Erp/CotarCom/Details/5
        public ActionResult Details(int id)
        {
            var item = service.Find(id);

            return View(item);
        }

        // GET: Erp/CotarCom/Create
        [AreaAuthorizeAttribute("Erp", Roles = "cotarcom-c")]
        public ActionResult Create(int id)
        {
            var idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            var item = new CotFornecedor { CotGrupoId = id, UsuarioId = idUsuario };

            return PartialView(item);
        }

        // POST: Erp/CotarCom/Create
        [HttpPost]
        public ActionResult Create(CotFornecedor item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true, id = item.CotGrupoId });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }

        // GET: Erp/CotarCom/Edit/5
        [AreaAuthorizeAttribute("Erp", Roles = "cotarcom-u")]
        public ActionResult Edit(int? id)
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

        // POST: Erp/CotarCom/Edit/5
        [HttpPost]
        public ActionResult Edit(CotFornecedor item)
        {
            try
            {
                item.AlteradoEm = DateTime.Now;
                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    service.Gravar(item);
                    return Json(new { success = true, id = item.CotGrupoId });
                }

                return PartialView(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }

        // GET: Erp/CotarCom/Delete/5
        [AreaAuthorizeAttribute("Erp", Roles = "cotarcom-d")]
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

        // POST: Erp/CotarCom/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = service.Excluir(id);
                return Json(new { success = true, id = item.UsuarioId });
            }
            catch (ArgumentException e)
            {
                var item = service.Find(id);

                if (item == null)
                {
                    return HttpNotFound();
                }

                ModelState.AddModelError(string.Empty, e.Message);
                return PartialView(item);
            }
        }

        // GET: Erp/CotarCom/CotarCom/5
        public ActionResult CotarCom(int idRequisicao)
        {
            ViewBag.IdRequisicao = idRequisicao;
            return PartialView();
        }

        // GET: Erp/CotarCom/GetSelecaoFornecedores
        public ActionResult GetSelecaoFornecedores(int idCotGrupo, int idRequisicao)
        {
            var fornecedores = service.Listar()
                .Where(x => x.CotGrupoId == idCotGrupo && x.Ativo == true)
                .OrderBy(x => x.Fornecedor.Fantasia)
                .ToList();

            ViewBag.IdCotGrupo = idCotGrupo;
            ViewBag.IdRequisicao = idRequisicao;
            return PartialView(fornecedores);
        }

        // POST: Erp/CotarCom/EnviarCotacao/
        [HttpPost]
        public ActionResult EnviarCotacao(int[] selecionados, int idRequisicao)
        {
            if (selecionados.Count() > 0)
            {
                serviceCotadoCom.EnviarCotacaoFornecedor(idRequisicao, selecionados.ToList(), login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name));
                return Json(new { success = true });
            }

            return Json(new { arguments = "Nenhum fornecedor selecionado para envio" });
        }

        // GET: Erp/CotarCom/EnviarPorEmail/
        public ActionResult EnviarPorEmail(int idRequisicao)
        {
            ViewBag.IdRequisicao = idRequisicao;
            return PartialView();
        }

        // POST: Erp/CotarCom/EnviarPorEmail/
        [HttpPost]
        public ActionResult EnviarPorEmail(int idRequisicao, string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return Json(new { error = "Informe o email" });
                }

                if (serviceCotadoCom.EnviarCotacaoFornecedor(idRequisicao, email) == true)
                {
                    return Json(new { success = true, message = $"Cotação enviada para { email }" });
                }
                else
                {
                    return Json(new { error = "Falha ao enviar email" });
                }
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        // GET: Erp/CotarCom/Imprimir/5
        public ActionResult Imprimir(int id)
        {
            var item = serviceRequisicao.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return PartialView(item);
        }

        // GET: Erp/CotarCom/ImprimirRequisicao/5
        public ActionResult ImprimirCotacaoEmBranco(int id, int idFornecedor)
        {
            var item = serviceRequisicao.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            var fornecedor = serviceFornecedor.Find(idFornecedor);

            if (fornecedor == null)
            {
                return HttpNotFound();
            }

            // grava envio desta cotacao ao fornecedor
            serviceCotadoCom.GravarEnvioAoFornecedor(id, idFornecedor, login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name), Guid.NewGuid().ToString(), fornecedor.Email);

            return View(new RequisicaoFornecedor { Fornecedor = fornecedor, Requisicao = item });
        }
    }
}
