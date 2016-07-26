using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Gen
{
    public class ArquivoMortoService : IBaseService<ArquivoMorto>
    {
        private IBaseRepository<ArquivoMorto> repository;

        public ArquivoMortoService()
        {
            this.repository = new EFRepository<ArquivoMorto>();
        }

        public ArquivoMorto Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ArquivoMorto Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(ArquivoMorto item)
        {
            item.AlteradoEm = DateTime.Now;
            item.Conteudo = item.Conteudo.ToUpper().Trim();
            item.Observ = string.IsNullOrEmpty(item.Observ) ? string.Empty : item.Observ.ToUpper().Trim();

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<ArquivoMorto> Listar()
        {
            return repository.Listar();
        }
    }
}
