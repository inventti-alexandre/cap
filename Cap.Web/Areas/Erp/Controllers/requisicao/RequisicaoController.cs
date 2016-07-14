using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Requisicao;
using Cap.Web.Areas.Erp.Models;
using Cap.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    [AreaAuthorizeAttribute("Erp", Roles = "requisicao-r")]
    public class RequisicaoController : Controller
    {
        IBaseService<ReqRequisicao> service;
        IBaseService<Fornecedor> serviceFornecedor;
        IBaseService<Logistica> serviceLogistica;
        ILogin login;
        IRequisicao serviceRequisicao;
        ISistemaConfig serviceConfig;

        public RequisicaoController(IBaseService<ReqRequisicao> service, IBaseService<Fornecedor> serviceFornecedor, ILogin login, IRequisicao serviceRequisicao, IBaseService<Logistica> serviceLogistica, ISistemaConfig serviceConfig)
        {
            this.service = service;
            this.serviceFornecedor = serviceFornecedor;
            this.serviceRequisicao = serviceRequisicao;
            this.serviceLogistica = serviceLogistica;
            this.serviceConfig = serviceConfig;
            this.login = login;
        }

        // GET: Erp/Requisicao
        public ActionResult Index()
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            ViewBag.IdEmpresa = usuario.IdEmpresa;
            ViewBag.IdUsuario = usuario.Id;
            return View();
        }

        // GET: Erp/Requisicao/Create
        public ActionResult Nova()
        {
            var item = new ReqRequisicao
            {
                IdSolicitadoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name),
                CotarAte = DateTime.Today.Date.AddDays(1),
                EntregarDia = DateTime.Today.Date.AddDays(2),
                SolicitadoEm = DateTime.Today.Date,
                Situacao = Situacao.EmDigitacao
            };

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

        // GET: Erp/Requisicao/ImprimirCotacao/5
        public ActionResult ImprimirCotacao(int id, int idFornecedor)
        {
            var item = service.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            var fornecedor = serviceFornecedor.Find(idFornecedor);

            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            
            return View(new RequisicaoFornecedor { Fornecedor = fornecedor, Requisicao = item });
        }

        // GET: Erp/Requisicao/EmAndamento
        public ActionResult EmAndamento(int idUsuario = 0)
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            var requisicoes = new List<ReqRequisicao>();

            // requisicoes a cotar
            requisicoes.AddRange(serviceRequisicao.GetRequisicoes(Situacao.Cotar, usuario.IdEmpresa));

            // requisicoes em cotacao
            requisicoes.AddRange(serviceRequisicao.GetRequisicoes(Situacao.EmCotacao, usuario.IdEmpresa));

            // requisicoes cotadas
            requisicoes.AddRange(serviceRequisicao.GetRequisicoes(Situacao.Cotado, usuario.IdEmpresa));

            return PartialView(requisicoes);
        }

        // GET: Erp/Requisicao/Compradas/
        public ActionResult Compradas(int idUsuario = 0)
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            int dias = serviceConfig.GetConfig(usuario.IdEmpresa).RequisicaoExibirComprasUltimosDias;
            DateTime inicial = DateTime.Today.Date.AddDays(dias * -1);

            // requisicoes compradas
            var requisicoes = serviceRequisicao.GetRequisicoes(Situacao.Comprada, usuario.IdEmpresa, 0, inicial)
                .OrderByDescending(x => x.CompradoEm);

            ViewBag.Dias = dias;
            return PartialView(requisicoes);
        }        

        // GET: Erp/Requisicao/Aprovadas
        public ActionResult Aprovadas(int idUsuario = 0)
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            // requisicoes aprovadas
            var requisicoes = serviceRequisicao.GetRequisicoes(Situacao.Aprovada, usuario.IdEmpresa)
                .OrderBy(x => x.EntregarDia);

            return PartialView(requisicoes);
        }

        // GET: Erp/Requisicao/Logistica/5
        public ActionResult Logistica(int id)
        {
            var requisicao = service.Find(id);

            if (requisicao == null)
            {
                return HttpNotFound();
            }

            Logistica logistica;

            if (requisicao.LogisticaId == null)
            {
                logistica = new Domain.Models.Cap.Logistica
                {
                    AlteradoEm = DateTime.Now,
                    DataServico = requisicao.EntregarDia < DateTime.Today.Date ? DateTime.Today.Date : requisicao.EntregarDia,
                    EmpresaId = requisicao.Departamento.IdEmpresa,
                    Observ = $"Requisição # { requisicao.Id}",
                    Id = 0,
                    Servico = getStringServico(requisicao),
                    UsuarioId = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name)
                };
            }
            else
            {
                logistica = requisicao.Logistica;
            }

            ViewBag.Departamento = requisicao.Departamento.Descricao;
            ViewBag.EntregarDia = requisicao.EntregarDia.ToShortDateString();
            ViewBag.SolicitadoPor = requisicao.SolicitadoPor.Nome;
            ViewBag.Situacao = requisicao.Situacao.ToString();
            ViewBag.Id = requisicao.Id;
            return View(logistica);
        }

        // GET: Erp/Requisicao/EntregasPrevistas/5
        public ActionResult EntregasPrevistas(DateTime? data)
        {
            var usuario = login.GetUsuario(System.Web.HttpContext.Current.User.Identity.Name);

            if (data == null)
            {
                var dias = serviceConfig.GetConfig(usuario.IdEmpresa).RequisicaoExibirEntregasPrevistasAteProximosDias;
                data = DateTime.Today.Date;
            }

            var requisicoes = serviceRequisicao.GetEntregas((DateTime)data, usuario.IdEmpresa, 0);

            return PartialView(requisicoes);
        }

        // POST: Erp/Requisicao/Logistica/5
        [HttpPost]
        public ActionResult Logistica(Logistica logistica, int idRequisicao)
        {
            var requisicao = service.Find(idRequisicao);
            ViewBag.Departamento = requisicao.Departamento.Descricao;
            ViewBag.EntregarDia = requisicao.EntregarDia.ToShortDateString();
            ViewBag.SolicitadoPor = requisicao.SolicitadoPor.Nome;
            ViewBag.Situacao = requisicao.Situacao.ToString();
            ViewBag.Id = requisicao.Id;

            try
            {
                if (ModelState.IsValid)
                {
                    serviceRequisicao.SendToLogistica(logistica, idRequisicao);
                    return RedirectToAction("Index");
                }

                return View(logistica);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(logistica);
            }
        }

        // GET: Erp/Requisicao/ConfirmarEntrega
        public ActionResult ConfirmarEntrega(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var requisicao = service.Find((int)id);

            if (requisicao == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
            return View(requisicao);
        }

        // POST: Erp/Requisicao/ConfirmarEntrega
        [HttpPost]
        public ActionResult ConfirmarEntrega(int id, int entregaConfirmadaPor)
        {
            try
            {
                if (entregaConfirmadaPor == 0)
                {
                    ViewBag.IdUsuario = entregaConfirmadaPor;
                    throw new ArgumentException("Selecione o responsável pela confirmação");
                }

                serviceRequisicao.ConfirmarEntrega(id, entregaConfirmadaPor);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.IdUsuario = entregaConfirmadaPor;
                return View(service.Find(id));
            }
        }

        #region [ methods ]

        private string getStringServico(ReqRequisicao requisicao)
        {
            var sb = new StringBuilder();

            sb.Append("Providenciar:")
                .AppendLine("\n\n")
                .AppendLine($"Departamento: { requisicao.Departamento.Descricao}, {requisicao.Departamento.Endereco}")
                .AppendLine("\n\n");

            foreach (var item in requisicao.ReqMaterial)
            {
                sb.AppendLine($"{ item.Qtde.ToString("n2")} { item.Material.Unidade.Descricao } {item.Material.Descricao} {item.Observ}\n");
            }

            return sb.ToString();
        }

        #endregion
    }
}
