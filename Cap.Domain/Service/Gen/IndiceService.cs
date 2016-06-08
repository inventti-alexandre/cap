using Cap.Domain.Abstract;
using Cap.Domain.Models.Gen;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Gen
{
    public class IndiceService : IBaseService<Indice>
    {
        private IBaseRepository<Indice> repository;

        public IndiceService()
        {
            repository = new EFRepository<Indice>();
        }

        public Indice Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Indice item = repository.Find(id);

                if (item != null)
                {
                    item.Ativo = false;
                    item.AlteradoEm = DateTime.Now;
                    return repository.Alterar(item);
                }

                return item;
            }
        }

        public Indice Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Indice item)
        {
            item.AlteradoEm = DateTime.Now;
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.Nome = item.Nome.ToUpper().Trim();

            if (repository.Listar().Where(x => x.Nome == item.Nome && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um índice cadastrado com este nome");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Indice> Listar()
        {
            return repository.Listar();
        }
    }
}
