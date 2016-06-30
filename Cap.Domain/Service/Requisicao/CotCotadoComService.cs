using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class CotCotadoComService : IBaseService<CotCotadoCom>
    {
        private IBaseRepository<CotCotadoCom> repository;

        public CotCotadoComService()
        {
            repository = new EFRepository<CotCotadoCom>();
        }

        public CotCotadoCom Excluir(int id)
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

        public CotCotadoCom Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(CotCotadoCom item)
        {
            item.AlteradoEm = DateTime.Now;

            if (item.Id == 0)
            {
                item.Preenchida = false;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<CotCotadoCom> Listar()
        {
            return repository.Listar();
        }
    }
}
