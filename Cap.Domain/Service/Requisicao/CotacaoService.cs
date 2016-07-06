using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Requisicao;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class CotacaoService : ICotacaoService
    {
        private IBaseService<CotCotacao> service;
        private IBaseService<CotDadosCotacao> serviceDadosCotacao;
        private IBaseService<CotCotadoCom> serviceCotadoCom;

        public CotacaoService()
        {
            this.service = new CotCotacaoService();
            this.serviceDadosCotacao = new CotDadosCotacaoService();
            this.serviceCotadoCom = new CotCotadoComService();
        }

        public CotacaoFornecedor GetCotacao(int idRequisicao, int idFornecedor)
        {
            var cotacao = service.Listar().Where(x => x.ReqRequisicaoId == idRequisicao && x.FornecedorId == idFornecedor).ToList();

            if (cotacao.Count() == 0)
            {
                throw new ArgumentException("Cotação inexistente");
            }

            if (cotacao.FirstOrDefault().Requisicao.Situacao == Situacao.Comprada)
            {
                throw new ArgumentException("Esta cotação já foi comprada");
            }

            var cotacaoFornecedor = new CotacaoFornecedor
            {
                RequisicaoId = idRequisicao,
                FornecedorId = idFornecedor,
                CotCotacao = cotacao,
                CotDadosCotacao = getDadosCotacao(idRequisicao, idFornecedor)
            };

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
