using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Controllers.Public
{
    public class CotacaoController : Controller
    {
        private IBaseService<CotCotacao> service;
        private IBaseService<CotCotadoCom> serviceCotadoCom;
        private IBaseService<CotDadosCotacao> serviceDadosCotacao;

        public CotacaoController(IBaseService<CotCotacao> service, IBaseService<CotCotadoCom> serviceCotadoCom, IBaseService<CotDadosCotacao> serviceDadosCotacao)
        {
            this.service = service;
            this.serviceCotadoCom = serviceCotadoCom;
            this.serviceDadosCotacao = serviceDadosCotacao;
        }

        // GET: Cotacao/5/5
        public ActionResult Index(int idRequisicao, int idFornecedor)
        {
            try
            {
                var cotacao = service.Listar().Where(x => x.ReqRequisicaoId == idRequisicao && x.FornecedorId == idFornecedor).ToList();

                if (cotacao.Count() == 0)
                {
                    return HttpNotFound();
                }

                if (cotacao.FirstOrDefault().Requisicao.Situacao == Situacao.Comprada)
                {
                    ViewBag.Message = "Esta cotação já foi comprada";
                    return View(new CotacaoFornecedor());
                }

                var cotacaoFornecedor = new CotacaoFornecedor
                {
                    RequisicaoId = idRequisicao,
                    FornecedorId = idFornecedor,
                    CotCotacao = cotacao,
                    CotDadosCotacao = getDadosCotacao(idRequisicao, idFornecedor)
                };

                return View(cotacaoFornecedor);
            }
            catch (Exception e)
            {
                ViewBag.Message = e;
                return View(new CotacaoFornecedor());
            }
        }

        // POST: Cotacao
        [HttpPost]
        public ActionResult Index(int RequisicaoId, int FornecedorId, ICollection<CotCotacao> cotacoes, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = ModelState.Values.ToList();
            }

            return View();
        }

        private CotDadosCotacao getDadosCotacao(int idRequisicao, int idFornecedor)
        {
            var cotadoCom = serviceCotadoCom.Listar().Where(x => x.ReqRequisicaoId == idRequisicao && x.FornecedorId == idFornecedor).FirstOrDefault();

            if (cotadoCom == null)
            {
                throw new ArgumentException("Esta cotação não esta sendo cotada com sua empresa");
            }

            var dadosCotacao = cotadoCom.DadosCotacao;

            if (dadosCotacao == null)
            {
                return new CotDadosCotacao { CotCotadoComId = cotadoCom.Id };
            }

            return dadosCotacao;
        }
    }
}