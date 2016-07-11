using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class CotDadosCotacaoService : IBaseService<CotDadosCotacao>
    {
        private IBaseRepository<CotDadosCotacao> repository;

        public CotDadosCotacaoService()
        {
            repository = new EFRepository<CotDadosCotacao>();
        }

        public CotDadosCotacao Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public CotDadosCotacao Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(CotDadosCotacao item)
        {
            item.AlteradoEm = DateTime.Now;
            item.Condicao = (item.Condicao == null ? string.Empty : item.Condicao.ToUpper().Trim());
            item.Contato = (item.Contato == null ? string.Empty : item.Contato.ToUpper().Trim());
            item.Observ = (item.Observ == null ? string.Empty : item.Observ.ToUpper().Trim());
            item.PrevisaoEntrega = (item.PrevisaoEntrega == null ? string.Empty : item.PrevisaoEntrega.ToUpper().Trim());
            item.Validade = (item.Validade == null ? string.Empty : item.Validade.ToUpper().Trim());

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<CotDadosCotacao> Listar()
        {
            return repository.Listar();
        }
    }
}
