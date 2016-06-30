using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class CotCotacaoService : IBaseService<CotCotacao>
    {
        private IBaseRepository<CotCotacao> repository;

        public CotCotacaoService()
        {
            repository = new EFRepository<CotCotacao>();
        }

        public CotCotacao Excluir(int id)
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

        public CotCotacao Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(CotCotacao item)
        {
            item.Observ = (item.Observ == null ? string.Empty : item.Observ.ToUpper().Trim());

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<CotCotacao> Listar()
        {
            return repository.Listar();
        }
    }
}
