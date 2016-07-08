using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Cap.Domain.Models.Requisicao
{

    #region "[ Properties ]"

    public class Resumo
    {
        public ReqRequisicao Requisicao { get; set; }
        public List<Influencia> Influencia { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal PrecoMinimo { get; set; }
        public List<IndicacaoMelhorPreco> Indicacao { get; set; }
        public List<CotacaoDetalhada> ResumoDetalhado { get; set; }
    }

    public class CotacaoDetalhada
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Qtde { get; set; }
        public string Unidade { get; set; }
        public string Descricao { get; set; }
        public int MaterialId { get; set; }
        public List<CotacaoItem> Cotacao { get; set; }
    }

    public class CotacaoItem
    {
        public Fornecedor Fornecedor { get; set; }
        public CotDadosCotacao DadosCotacao { get; set; }
        public int Material { get; set; }
        [Display(Name ="Unitário")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Unitario { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Total { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name ="Melhor preço")]
        public bool MelhorPreco { get; set; }
        public string Unidade { get; set; }
    }

    public class IndicacaoMelhorPreco
    {
        [Display(Name = "Melhor preço")]
        public bool MelhorPreco { get; set; }
        [Display(Name = "Cotou todos os ítens")]
        public bool CotouTodosItens { get; set; }
        public Fornecedor Fornecedor { get; set; }
        [Display(Name = "Valor da cotação")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal ValorCotacao { get; set; }
        [Display(Name = "Desconto à negociar")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal DescontoANegociar { get; set; }
        [Display(Name = "% a negociar")]
        [DisplayFormat(DataFormatString = "{0:N2}%")]
        public decimal DescontoANegociarPorcentagem { get; set; }
        [Display(Name = "Condições de pagamento")]
        public string CondicoesPagamento { get; set; }
    }

    public class Influencia
    {
        public int Id { get; set; }
        [Display(Name = "Qtde.")]
        public decimal Qtde { get; set; }
        public string Unidade { get; set; }
        [Display(Name = "Material")]
        public int MaterialId { get; set; }
        [Display(Name = "Material")]
        public string Descricao { get; set; }
        [Display(Name = "Unitário")]
        public decimal Unitario { get; set; }
        [Display(Name = "Unitário com impostos")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal UnitarioComImpostos { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Total { get; set; }
        [Display(Name = "Total com impostos")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalComImpostos { get; set; }
        [Display(Name = "Influência")]
        [DisplayFormat(DataFormatString = "{0:N2}%")]
        public decimal InfluenciaInsumo { get; set; }
        public Fornecedor Fornecedor { get; set; }
        [Display(Name = "Observações")]
        public string Observ { get; set; }
    }

    #endregion

    public class ResumoCotacao : IResumoCotacao
    {
        private int _idRequisicao;
        private Resumo _resumo;
        private List<CotCotacao> _cotacoes;
        private List<CotDadosCotacao> _dadosCotacao;
        private EFDbContext ctx = new EFDbContext();
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
            _dadosCotacao = getDadosCotacao();
            _cotacoes = getCotacoes();

            // instancia o resumo
            _resumo = new Resumo();
            _resumo.Requisicao = getRequisicao();
            _resumo.Influencia = getInfluencia();
            _resumo.Indicacao = getIndicacao();
            _resumo.ResumoDetalhado = getResumoDetalhado();

            return _resumo;
        }

        private List<CotDadosCotacao> getDadosCotacao()
        {
            return (from d in ctx.CotDadosCotacao
                    join c in ctx.CotCotadoCom on d.CotCotadoComId equals c.Id
                    where c.ReqRequisicaoId == _idRequisicao
                    select d).ToList();
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
            var influencias = _resumo.Influencia
                .GroupBy(x => x.Fornecedor.Id)
                .Select(y => new
                {
                    Fornecedor = y.Key,
                    Influencia = _resumo.Influencia.Where(k => k.Fornecedor.Id == y.Key).Sum(k => k.InfluenciaInsumo)
                })
                .OrderByDescending(x => x.Influencia);

            foreach (var item in _dadosCotacao)
            {
                var indicacao = new IndicacaoMelhorPreco();
                indicacao.CondicoesPagamento = item.Condicao;
                indicacao.Fornecedor = item.CotadoCom.Fornecedor;

                // cotacao de todos os insumos para este fornecedor
                var cotacao = _cotacoes.Where(x => x.FornecedorId == indicacao.Fornecedor.Id);
                indicacao.ValorCotacao = cotacao.Sum(x => x.PrecoComImpostos * x.ReqMaterial.Qtde);
                indicacao.DescontoANegociar = indicacao.ValorCotacao - _resumo.PrecoMinimo;
                indicacao.DescontoANegociarPorcentagem = (1 - (indicacao.ValorCotacao / _resumo.PrecoMinimo)) * 100;
                indicacao.CotouTodosItens = (cotacao.Where(x => x.PrecoComImpostos > 0).Count() == 0);
                lista.Add(indicacao);
            }

            // definicao do melhor preco (quem tem o menor desconto a negociar)
            var menorDesconto = lista.Min(x => x.DescontoANegociar);
            for (int i = 0; i < lista.Count; i++)
            {
                lista[i].MelhorPreco = (lista[i].DescontoANegociar == menorDesconto);
            }

            return lista;
        }

        private List<CotacaoDetalhada> getResumoDetalhado()
        {
            var lista = new List<CotacaoDetalhada>();

            foreach (var item in _resumo.Requisicao.ReqMaterial)
            {
                var detalhada = new CotacaoDetalhada();
                detalhada.Id = item.Id;
                detalhada.Descricao = item.Material.Descricao;
                detalhada.MaterialId = item.Material.Id;
                detalhada.Qtde = item.Qtde;
                detalhada.Unidade = item.Material.Unidade.Descricao;
                detalhada.Cotacao = getCotacaoItem(detalhada.MaterialId, detalhada.Unidade);
                lista.Add(detalhada);
            }

            return lista;
        }

        private List<CotacaoItem> getCotacaoItem(int materialId, string unidade)
        {
            var lista = new List<CotacaoItem>();

            foreach (var item in _dadosCotacao)
            {
                var cotacaoInsumo = new CotacaoItem();

                // dados da cotacao
                cotacaoInsumo.DadosCotacao = item;
                cotacaoInsumo.Fornecedor = item.CotadoCom.Fornecedor;
                cotacaoInsumo.Material = materialId;
                cotacaoInsumo.Unidade = unidade;

                // composicao do preco
                cotacaoInsumo.Unitario = _cotacoes
                    .Where(x => x.FornecedorId == cotacaoInsumo.Fornecedor.Id
                    && x.ReqMaterial.Material.Id == materialId)
                    .FirstOrDefault()
                    .PrecoComImpostos;

                cotacaoInsumo.Total = _resumo.Requisicao.ReqMaterial.Where(x => x.IdMaterial == materialId)
                    .Select(x => x.Qtde).FirstOrDefault() * cotacaoInsumo.Unitario;

                // melhor preco
                cotacaoInsumo.MelhorPreco = _resumo.Influencia.Where(x => x.MaterialId == materialId)
                    .Select(x => x.Fornecedor.Id).FirstOrDefault() == cotacaoInsumo.Fornecedor.Id;

                lista.Add(cotacaoInsumo);
            }

            return lista;
        }
    }

}
