using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class GrupoCustoService : IBaseService<GrupoCusto>
    {
        IBaseRepository<GrupoCusto> repository;

        public GrupoCustoService()
        {
            repository = new EFRepository<GrupoCusto>();
        }

        public GrupoCusto Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                GrupoCusto grupo = repository.Find(id);

                if (grupo != null)
                {
                    grupo.Ativo = false;
                    grupo.AlteradoEm = DateTime.Now;
                    return repository.Alterar(grupo);
                }

                return grupo;
            }
        }

        public GrupoCusto Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(GrupoCusto item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um grupo de custo cadastrado com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<GrupoCusto> Listar()
        {
            return repository.Listar();
        }
    }
}
