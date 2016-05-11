using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Service.Requisicao
{
    public class ReqRequisicaoService : IBaseService<ReqRequisicao>
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
                    item.Ativo = false;
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
            
            // TODO: parei aki - tem que ter data para entrega e tem que validar

            if (item.Id == 0)
            {
                item.Ativo = true;
                item.SolicitadoEm = DateTime.Now;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<ReqRequisicao> Listar()
        {
            return repository.Listar();
        }
    }
}
