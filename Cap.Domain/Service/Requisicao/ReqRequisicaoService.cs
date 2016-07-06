using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Req;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class ReqRequisicaoService : IBaseService<ReqRequisicao>, IRequisicao
    {
        private IBaseRepository<ReqRequisicao> repository;
        private EFDbContext ctx = new EFDbContext();

        public ReqRequisicaoService()
        {
            repository = new EFRepository<ReqRequisicao>();
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

        public List<ReqRequisicao> GetRequisicoes(Situacao situacao, int idEmpresa, int idUsuario = 0)
        {
            return (from r in ctx.ReqRequisicao
                    join d in ctx.Departamento on r.IdDepartamento equals d.Id
                    where
                    d.IdEmpresa == idEmpresa
                    && r.Situacao == situacao
                    && (idUsuario == 0 || r.IdSolicitadoPor == idUsuario)
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

        // TODO: enviar compra direta
        // TODO: retira
    }
}
