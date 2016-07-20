using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    public class CotacaoCompraController : Controller
    {
        private ICotacaoService serviceCotacao;
        private IBaseService<ReqRequisicao> serviceRequisicao;
        ILogin login;

        public CotacaoCompraController(ICotacaoService serviceCotacao, IBaseService<ReqRequisicao> serviceRequisicao, ILogin login)
        {
            this.serviceCotacao = serviceCotacao;
            this.serviceRequisicao = serviceRequisicao;
            this.login = login;
        }

        // GET: Erp/CotacaoCompra
        public ActionResult Index(int id)
        {
            var requisicao = serviceRequisicao.Find(id);

            if (requisicao == null)
            {
                return HttpNotFound();
            }

            return View(requisicao);
        }

        // GET: Erp/CotacaoCompra/GetCotacaoFornecedor/5/5
        public ActionResult GetCotacaoFornecedor(int idRequisicao, int idFornecedor)
        {
            try
            {
                if (idRequisicao == 0)
                {
                    throw new ArgumentException("Requisição inválida");
                }

                if (idFornecedor == 0)
                {
                    throw new ArgumentException("Selecione o fornecedor");
                }

                int idUsuario = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);

                var cotacao = serviceCotacao.GetCotacao(idRequisicao, idFornecedor, idUsuario);

                return PartialView(cotacao);
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetCotacaoFornecedor(CotacaoFornecedor cotacao)
        {
            if (cotacao == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            cotacao.CotDadosCotacao.AlteradoEm = DateTime.Now;
            cotacao.CotDadosCotacao.Condicao = cotacao.CotDadosCotacao.Condicao ?? string.Empty;
            cotacao.CotDadosCotacao.Contato = cotacao.CotDadosCotacao.Contato ?? string.Empty;
            cotacao.CotDadosCotacao.Observ = cotacao.CotDadosCotacao.Observ ?? string.Empty;
            cotacao.CotDadosCotacao.PrevisaoEntrega = cotacao.CotDadosCotacao.PrevisaoEntrega ?? string.Empty;
            cotacao.CotDadosCotacao.Validade = cotacao.CotDadosCotacao.Validade ?? string.Empty;

            TryUpdateModel(cotacao);

            if (ModelState.IsValid)
            {
                try
                {
                    serviceCotacao.GravarCotacao(cotacao);
                    ViewBag.Message = "Cotação enviada. Obrigado!";
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    ViewBag.Error = e.Message;
                }
            }

            return PartialView(cotacao);
        }
    }
}