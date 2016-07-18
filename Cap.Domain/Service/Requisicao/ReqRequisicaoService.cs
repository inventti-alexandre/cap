using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using Cap.Domain.Service.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cap.Domain.Service.Requisicao
{
    public class ReqRequisicaoService : IBaseService<ReqRequisicao>, IRequisicao
    {
        private IBaseRepository<ReqRequisicao> repository;
        private EFDbContext ctx = new EFDbContext();
        private IBaseService<Logistica> serviceLogistica;
        private IBaseService<Fornecedor> serviceFornecedor;

        public ReqRequisicaoService()
        {
            repository = new EFRepository<ReqRequisicao>();
            serviceLogistica = new LogisticaService();
            serviceFornecedor = new FornecedorService();
        }

        public ReqRequisicao Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                ReqRequisicao item = repository.Find(id);

                if (item != null)
                {
                    item.Situacao = Situacao.Cancelada;
                    return repository.Alterar(item);
                }

                return item;
            }
        }

        public ReqRequisicao Find(int id)
        {
            return repository.Find(id);
        }

        public List<ReqRequisicao> GetRequisicoes(Situacao situacao, int idEmpresa, int idUsuario = 0, DateTime? inicial = null, DateTime? final = null)
        {
            return (from r in ctx.ReqRequisicao
                    join d in ctx.Departamento on r.IdDepartamento equals d.Id
                    where
                    d.IdEmpresa == idEmpresa
                    && r.Situacao == situacao
                    && (idUsuario == 0 || r.IdSolicitadoPor == idUsuario)
                    && (inicial == null || (r.CompradoEm != null && r.CompradoEm > inicial))
                    && (final == null || (r.CompradoEm != null && r.CompradoEm < final))
                    select r).ToList();
        }

        public int Gravar(ReqRequisicao item)
        {
            item.LiberadoObserv = string.IsNullOrEmpty(item.LiberadoObserv) ? string.Empty : item.LiberadoObserv.ToUpper().Trim();
            item.Observ = string.IsNullOrEmpty(item.Observ) ? string.Empty : item.Observ.ToUpper().Trim();
            if (item.CotarAte > item.EntregarDia)
            {
                if (item.EntregarDia > DateTime.Today.Date)
                {
                    item.CotarAte = item.EntregarDia.AddDays(-1);
                }
                else
                {
                    item.CotarAte = DateTime.Today.Date;
                }
            }

            if (item.Id == 0)
            {
                item.SolicitadoEm = DateTime.Now;
                item.Situacao = Situacao.EmDigitacao;
                item.IdCotadoPor = 0;
                item.CotadoEm = null;
                item.LiberadoParaCompra = false;
                item.LiberadoEm = null;
                item.IdLiberadoPor = 0;
                item.LiberadoObserv = string.Empty;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<ReqRequisicao> Listar()
        {
            return repository.Listar();
        }

        public void SendToLogistica(Logistica logistica, int idRequisicao)
        {
            try
            {
                var requisicao = repository.Find(idRequisicao);

                if (requisicao == null)
                {
                    throw new ArgumentException("Requisição inexistente");
                }

                // grava logistica
                logistica.AlteradoEm = DateTime.Now;
                logistica.Ativo = true;
                logistica.ConcluidoObserv = string.Empty;
                logistica.Observ = logistica.Observ == null ? string.Empty : logistica.Observ.ToUpper().Trim();
                logistica.Id = 0;
                serviceLogistica.Gravar(logistica);

                // grava LogisticaId em requisicao
                requisicao.LogisticaId = logistica.Id;
                requisicao.Situacao = Situacao.Comprada;
                requisicao.CompradoEm = DateTime.Today.Date;
                repository.Alterar(requisicao);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ReqRequisicao> GetEntregas(DateTime data, int idEmpresa, int idUsuario = 0)
        {
            return (from r in ctx.ReqRequisicao
                    join d in ctx.Departamento on r.IdDepartamento equals d.Id
                    where
                    d.IdEmpresa == idEmpresa
                    && (r.Situacao == Situacao.Comprada || r.Situacao == Situacao.Entregue)
                    && (idUsuario == 0 || r.IdSolicitadoPor == idUsuario)
                    && r.EntregarDia == data
                    select r).ToList();
        }

        public void ConfirmarEntrega(int id, int idUsuario)
        {
            try
            {
                var requisicao = repository.Find(id);

                if (requisicao == null)
                {
                    throw new ArgumentException("Requisição inexistente");
                }

                if (requisicao.Situacao == Situacao.Entregue)
                {
                    throw new ArgumentException("Esta requisição já foi entregue");
                }

                if (requisicao.Situacao != Situacao.Comprada)
                {
                    throw new ArgumentException("Esta requisição ainda não foi comprada");
                }

                requisicao.Situacao = Situacao.Entregue;
                requisicao.EntregueEm = DateTime.Now;
                requisicao.EntregaConfirmadaPor = idUsuario;
                repository.Alterar(requisicao);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public string GetStringServico(ReqRequisicao requisicao, int? idFornecedor)
        {
            try
            {
                var sb = new StringBuilder();

                sb.Append("Providenciar:")
                    .AppendLine("\n\n")
                    .AppendLine($"Departamento: { requisicao.Departamento.Descricao}, {requisicao.Departamento.Endereco}")
                    .AppendLine("\n\n");

                if (idFornecedor != null)
                {
                    var fornecedor = serviceFornecedor.Find((int)idFornecedor);
                    if (fornecedor == null)
                    {
                        throw new ArgumentException("Fornecedor inválido");
                    }
                    sb.Append($"Fornecedor: { fornecedor.Fantasia } - {fornecedor.Agenda.Endereco}, {fornecedor.Agenda.Bairro}, {fornecedor.Agenda.Cidade}")
                        .AppendLine(@"\n\n\");
                }

                foreach (var item in requisicao.ReqMaterial)
                {
                    sb.AppendLine($"{ item.Qtde.ToString("n2")} { item.Material.Unidade.Descricao } {item.Material.Descricao} {item.Observ}\n");
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        // TODO: enviar compra direta
        // TODO: retira
    }
}
