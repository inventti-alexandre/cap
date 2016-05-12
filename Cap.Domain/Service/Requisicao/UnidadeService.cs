using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class UnidadeService : ILogin<Unidade>
    {
        private IBaseRepository<Unidade> repository;

        public UnidadeService()
        {
            repository = new EFRepository<Unidade>();
        }

        public Unidade Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Unidade unidade = repository.Find(id);

                if (unidade != null)
                {
                    unidade.Ativo = false;
                    unidade.AlteradoEm = DateTime.Now;
                    return repository.Alterar(unidade);
                }

                return unidade;
            }
        }

        public Unidade Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Unidade item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Unidade já cadastrada");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Unidade> Listar()
        {
            return repository.Listar();
        }
    }
}
