using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Requisicao;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Cap.Domain.Service.Requisicao
{
    public class CotacaoService : ICotacaoService
    {
        private IBaseService<CotCotacao> service;
        private IBaseService<CotDadosCotacao> serviceDadosCotacao;
        private IBaseService<CotCotadoCom> serviceCotadoCom;
        private IBaseService<ReqMaterial> serviceReqMaterial;

        public CotacaoService()
        {
            this.service = new CotCotacaoService();
            this.serviceDadosCotacao = new CotDadosCotacaoService();
            this.serviceCotadoCom = new CotCotadoComService();
            this.serviceReqMaterial = new ReqMaterialService();
        }

        public CotacaoFornecedor GetCotacao(int idRequisicao, int idFornecedor)
        {
            //var cotacao = service.Listar().Where(x => x.ReqRequisicaoId == idRequisicao && x.FornecedorId == idFornecedor).ToList();
            var cotCom = serviceCotadoCom.Listar().Where(x => x.ReqRequisicaoId == idRequisicao && x.FornecedorId == idFornecedor).FirstOrDefault();

            if (cotCom == null)
            {
                throw new ArgumentException("Cotação inexistente");
            }

            if (cotCom.Requisicao.Situacao == Situacao.Comprada)
            {
                throw new ArgumentException("Esta requisição já foi comprada");
            }

            var cotacaoFornecedor = new CotacaoFornecedor
            {
                RequisicaoId = idRequisicao,
                FornecedorId = idFornecedor,
                CotCotacao = getCotacaoFornecedor(idRequisicao, idFornecedor),
                CotDadosCotacao = getDadosCotacao(idRequisicao, idFornecedor)
            };

            return cotacaoFornecedor;
        }

        private List<CotCotacao> getCotacaoFornecedor(int idRequisicao, int idFornecedor)
        {
            var cotacaoFornecedor = service.Listar().Where(x => x.ReqRequisicaoId == idRequisicao && x.FornecedorId == idFornecedor).ToList();

            if (cotacaoFornecedor.Count > 0)
            {
                // fornecedor ja preencheu ou quer alterar esta cotacao que ainda esta em andamento
                return cotacaoFornecedor;
            }

            // nao ha tuplas em CotCotacao para este fornecedor
            var materiais = serviceReqMaterial.Listar().Where(x => x.IdReqRequisicao == idRequisicao).ToList();
            foreach (var item in materiais)
            {
                cotacaoFornecedor.Add(new CotCotacao
                {
                    FornecedorId = idFornecedor,
                    Observ = string.Empty,
                    Preco = 0,
                    ReqMaterialId = item.Id,
                    ReqRequisicaoId = idRequisicao
                });
            }

            return cotacaoFornecedor;            
        }

        private CotDadosCotacao getDadosCotacao(int idRequisicao, int idFornecedor)
        {
            var cotadoCom = serviceCotadoCom.Listar().Where(x => x.ReqRequisicaoId == idRequisicao && x.FornecedorId == idFornecedor).FirstOrDefault();

            if (cotadoCom == null)
            {
                throw new ArgumentException("Esta cotação nã esta sendo cotada com sua empresa");
            }

            var dadosCotacao = cotadoCom.DadosCotacao;

            if (dadosCotacao == null)
            {
                return new CotDadosCotacao { CotCotadoComId = cotadoCom.Id };
            }

            return dadosCotacao;
        }

        public void GravarCotacao(CotacaoFornecedor cotacao)
        {
            // grava dados da cotacao
            serviceDadosCotacao.Gravar(cotacao.CotDadosCotacao);

            // grava cotacao
            for (int i = 0; i < cotacao.CotCotacao.Count; i++)
            {
                service.Gravar(cotacao.CotCotacao[i]);
            }

            // define status como preenchida
            var cotcom = serviceCotadoCom.Listar()
                .Where(x => x.ReqRequisicaoId == cotacao.RequisicaoId
                && x.FornecedorId == cotacao.FornecedorId)
                .FirstOrDefault();

            if (cotcom != null)
            {
                cotcom.Preenchida = true;
                serviceCotadoCom.Gravar(cotcom);
            }
        }
    }
}
