using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Requisicao;
using System;
using System.Net;
using System.Web.Mvc;

namespace Cap.Web.Controllers.Public
{
    public class CotacaoController : Controller
    {
        private IBaseService<CotCotacao> service;
        private IBaseService<CotCotadoCom> serviceCotadoCom;
        private IBaseService<CotDadosCotacao> serviceDadosCotacao;
        private ICotacaoService serviceCotacao;
        private IResumoCotacao serviceResumo;

        public CotacaoController(IBaseService<CotCotacao> service, IBaseService<CotCotadoCom> serviceCotadoCom, IBaseService<CotDadosCotacao> serviceDadosCotacao, ICotacaoService serviceCotacao, IResumoCotacao serviceResumo)
        {
            this.service = service;
            this.serviceCotadoCom = serviceCotadoCom;
            this.serviceDadosCotacao = serviceDadosCotacao;
            this.serviceCotacao = serviceCotacao;
            this.serviceResumo = serviceResumo;
        }

        // GET: Cotacao/5/5
        public ActionResult Index(string guid)
        {
            try
            {
                return View(serviceCotacao.GetCotacao(guid));
            }
            catch (Exception e)
            {
                ViewBag.Message = e;
                return View(new CotacaoFornecedor());
            }
        }

        // POST: Cotacao
        [HttpPost]
        public ActionResult Index(CotacaoFornecedor cotacao)
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
                    return View(cotacao);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    ViewBag.Error = e.Message;
                }
            }

            return View(cotacao);
        }
    }
}