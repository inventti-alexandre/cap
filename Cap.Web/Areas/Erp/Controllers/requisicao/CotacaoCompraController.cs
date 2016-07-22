using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Requisicao;
using System;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Areas.Erp.Controllers.requisicao
{
    public class CotacaoCompraController : Controller
    {
        private ICotacaoService serviceCotacao;
        private IBaseService<ReqRequisicao> serviceRequisicao;
        ILogin login;
        IReqComprar comprar;

        public CotacaoCompraController(ICotacaoService serviceCotacao, IBaseService<ReqRequisicao> serviceRequisicao, ILogin login, IReqComprar comprar)
        {
            this.serviceCotacao = serviceCotacao;
            this.serviceRequisicao = serviceRequisicao;
            this.login = login;
            this.comprar = comprar;
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
                    // grava cotacao
                    serviceCotacao.GravarCotacao(cotacao);

                    // agenda pedido/parcela
                    int idPedido = AgendarPagamento(cotacao.RequisicaoId, cotacao.FornecedorId);

                    return Json(new { success = true, idPedido = idPedido }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    ViewBag.Error = e.Message;
                }
            }

            return PartialView(cotacao);
        }

        private int AgendarPagamento(int idRequisicao, int idFornecedor)
        {
            try
            {
                return comprar.AgendarPagamento(idRequisicao, idFornecedor, login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}