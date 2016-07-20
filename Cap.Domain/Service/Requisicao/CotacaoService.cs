using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Requisicao;
using System;
using System.Linq;
using System.Collections.Generic;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Cap;
using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;

namespace Cap.Domain.Service.Requisicao
{
    public class CotacaoService : ICotacaoService
    {
        private IBaseService<CotCotacao> service;
        private IBaseService<CotDadosCotacao> serviceDadosCotacao;
        private IBaseService<CotCotadoCom> serviceCotadoCom;
        private IBaseService<ReqMaterial> serviceReqMaterial;
        private IBaseService<ReqRequisicao> serviceRequisicao;
        private IBaseService<Fornecedor> serviceFornecedor;
        private IBaseService<Usuario> serviceUsuario;

        public CotacaoService()
        {
            this.service = new CotCotacaoService();
            this.serviceDadosCotacao = new CotDadosCotacaoService();
            this.serviceCotadoCom = new CotCotadoComService();
            this.serviceReqMaterial = new ReqMaterialService();
            this.serviceRequisicao = new ReqRequisicaoService();
            this.serviceFornecedor = new FornecedorService();
            this.serviceUsuario = new UsuarioService();
        }

        public CotacaoFornecedor GetCotacao(string guid)
        {
            var cotCom = serviceCotadoCom.Listar().Where(x => x.Guid == guid).FirstOrDefault();

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
                RequisicaoId = cotCom.ReqRequisicaoId,
                FornecedorId = cotCom.FornecedorId,
                CotCotacao = getCotacaoFornecedor(cotCom.ReqRequisicaoId, cotCom.FornecedorId),
                CotDadosCotacao = getDadosCotacao(cotCom.ReqRequisicaoId, cotCom.FornecedorId)
            };

            return cotacaoFornecedor;
        }

        public CotacaoFornecedor GetCotacao(int idRequisicao, int idFornecedor, int idUsuario)
        {
            var cotCom = serviceCotadoCom.Listar().Where(x => x.ReqRequisicaoId == idRequisicao && x.FornecedorId == idFornecedor).FirstOrDefault();

            if (cotCom != null)
            {
                // ja foi enviada cotacao para este fornecedor
                return GetCotacao(cotCom.Guid);
            }

            var requisicao = serviceRequisicao.Find(idRequisicao);
            if (requisicao == null)
            {
                throw new ArgumentException("Requisição inexistente");
            }

            var fornecedor = serviceFornecedor.Find(idFornecedor);
            if (fornecedor == null)
            {
                throw new ArgumentException("Fornecedor inexistente");
            }

            var guid = Guid.NewGuid().ToString();
            var email = string.IsNullOrEmpty(fornecedor.Email) ? serviceUsuario.Find(idUsuario).Email : fornecedor.Email;
            serviceCotadoCom.Gravar(new CotCotadoCom
            {
                Email = email,
                FornecedorId = idFornecedor,
                Guid = guid,
                ReqRequisicaoId = idRequisicao,
                UsuarioId = idUsuario
            });

            return GetCotacao(guid);            
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
            if (cotacao.CotDadosCotacao.Id == 0)
            {
                //var cotadoComId = cotacao.CotDadosCotacao.CotadoCom.Id;
                var cotadoComId = cotacao.CotDadosCotacao.CotCotadoComId;
                if (cotadoComId != 0)
                {
                    var dados = serviceDadosCotacao.Listar().Where(x => x.CotCotadoComId == cotadoComId).FirstOrDefault();
                    if (dados != null)
                    {
                        cotacao.CotDadosCotacao.Id = dados.Id;
                    }
                }
            }
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
