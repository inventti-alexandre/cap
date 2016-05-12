using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class ReqRequisicaoService : ILogin<ReqRequisicao>
    {
        private IBaseRepository<ReqRequisicao> repository;

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

        // TODO: enviar requisicao para cotacao
        // TODO: enviar compra direta
        // TODO: retira
    }
}
