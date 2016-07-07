using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Models.Requisicao
{
    public class ResumoCotacao: IResumoCotacao
    {
        private int _idRequisicao;
        private Resumo _resumo;
        private List<CotCotacao> _cotacoes;
        private CotDadosCotacao _dadosCotacao;

        private IBaseService<ReqRequisicao> serviceRequisicao;
        private IBaseService<CotCotacao> serviceCotCotacao;

        public ResumoCotacao(IBaseService<ReqRequisicao> serviceRequisicao, IBaseService<CotCotacao> serviceCotCotacao)
        {
            this.serviceRequisicao = serviceRequisicao;
            this.serviceCotCotacao = serviceCotCotacao;
        }

        public Resumo GetResumo(int idRequisicao)
        {
            _idRequisicao = idRequisicao;
            _cotacoes = getCotacoes();

            // instancia o resumo
            _resumo = new Resumo();
            _resumo.Requisicao = getRequisicao();
            _resumo.Influencia = getInfluencia();
            _resumo.Indicacao = getIndicacao();
            _resumo.ResumoDetalhado = getResumoDetalhado();

            return _resumo;
        }
        private ReqRequisicao getRequisicao()
        {
            var requisicao = serviceRequisicao.Find(_idRequisicao);

            if (requisicao == null)
            {
                throw new ArgumentException("Requisição inexistente");
            }

            return requisicao;
        }

        private List<CotCotacao> getCotacoes()
        {
            var cotacoes = serviceCotCotacao.Listar()
                .Where(x => x.ReqRequisicaoId == _idRequisicao)
                .ToList();

            if (cotacoes.Count() == 0)
            {
                throw new ArgumentException("Não existem cotações disponíveis para esta requisição");
            }

            return cotacoes;
        }

        private List<Influencia> getInfluencia()
        {
            var lista = new List<Influencia>();

            foreach (var item in _resumo.Requisicao.ReqMaterial.ToList())
            {
                var influencia = new Influencia();

                // material
                influencia.Descricao = item.Material.Descricao;
                influencia.Id = item.Id;
                influencia.MaterialId = item.IdMaterial;
                influencia.Qtde = item.Qtde;
                influencia.Unidade = item.Material.Unidade.Descricao;

                // menor cotacao disponivel para este insumo dentre as cotacoes disponiveis
                CotCotacao cotacao;
                cotacao = _cotacoes.Where(x => x.ReqMaterialId == influencia.MaterialId && x.PrecoComImpostos > 0)
                    .OrderBy(x => x.Preco)
                    .FirstOrDefault();
                if (cotacao == null)
                {
                    // nenhum fornecedor cotou este item
                    cotacao = _cotacoes.FirstOrDefault(x => x.ReqMaterialId == influencia.MaterialId);
                }

                // fornecedor
                influencia.Fornecedor = cotacao.Fornecedor;
                influencia.Observ = item.Observ;

                // preco
                influencia.Unitario = cotacao.Preco;
                influencia.UnitarioComImpostos = cotacao.PrecoComImpostos;
                influencia.Total = item.Qtde * influencia.Unitario;
                influencia.TotalComImpostos = item.Qtde * influencia.UnitarioComImpostos;

                lista.Add(influencia);
            }

            // total da cotacao com menor preco
            _resumo.PrecoMinimo = lista.Sum(x => x.TotalComImpostos);

            for (int i = 0; i < lista.Count; i++)
            {
                lista[i].InfluenciaInsumo = (lista[i].TotalComImpostos / _resumo.PrecoMinimo) * 100;
            }

            return lista;
        }

        private List<IndicacaoMelhorPreco> getIndicacao()
        {
            var lista = new List<IndicacaoMelhorPreco>();

            // melhor preco - influencia
            

            return lista;
        }

        private List<CotacaoDetalhada> getResumoDetalhado()
        {
            throw new NotImplementedException();
        }

    }

    public class Resumo
    {
        public ReqRequisicao Requisicao { get; set; }
        public List<Influencia> Influencia { get; set; }
        public decimal PrecoMinimo { get; set; }
        public List<IndicacaoMelhorPreco> Indicacao { get; set; }
        public List<CotacaoDetalhada> ResumoDetalhado { get; set; }
    }

    public class CotacaoDetalhada
    {
        public int Id { get; set; }
        public decimal Qtde { get; set; }
        public string Unidade { get; set; }
        public string Descricao { get; set; }
        public int MaterialId { get; set; }
        public List<CotacaoItem> Cotacao { get; set; }
    }

    public class CotacaoItem
    {
        public Fornecedor Fornecedor { get; set; }
        public int Material { get; set; }
        public decimal Unitario { get; set; }
        public decimal Total { get; set; }
        public bool MelhorPreco { get; set; }
        public string Unidade { get; set; }
    }

    public class IndicacaoMelhorPreco
    {
        public bool MelhorPreco { get; set; }
        public bool CotouTodosItens { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public decimal ValorCotacao { get; set; }
        public decimal DescontoANegociar { get; set; }
        public decimal DescontoANegociarPorcentagem { get; set; }
        public string CondicoesPagamento { get; set; }
    }

    public class Influencia
    {
        public int Id { get; set; }
        public decimal Qtde { get; set; }
        public string Unidade { get; set; }
        public int MaterialId { get; set; }
        public string Descricao { get; set; }
        public decimal Unitario { get; set; }
        public decimal UnitarioComImpostos { get; set; }
        public decimal Total { get; set; }
        public decimal TotalComImpostos { get; set; }
        public decimal InfluenciaInsumo { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public string Observ { get; set; }
    }
}
