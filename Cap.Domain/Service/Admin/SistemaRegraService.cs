using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Admin
{
    public class SistemaRegraService : IBaseService<SistemaRegra>
    {
        private IBaseRepository<SistemaRegra> repository;

        public SistemaRegraService()
        {
            repository = new EFRepository<SistemaRegra>();
        }

        public SistemaRegra Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                SistemaRegra regra = repository.Find(id);

                if (regra != null)
                {
                    regra.Ativo = false;
                    regra.AlteradoEm = DateTime.Now;
                    repository.Alterar(regra);
                }

                return regra;
            }
        }

        public SistemaRegra Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(SistemaRegra item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.Observ = item.Observ.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && item.Id != x.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma regra cadastrada com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<SistemaRegra> Listar()
        {
            return repository.Listar();
        }
    }
}
