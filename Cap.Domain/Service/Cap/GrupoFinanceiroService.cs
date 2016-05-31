using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class GrupoFinanceiroService : IBaseService<GrupoFinanceiro>
    {
        IBaseRepository<GrupoFinanceiro> repository;

        public GrupoFinanceiroService()
        {
            repository = new EFRepository<GrupoFinanceiro>();
        }

        public GrupoFinanceiro Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                GrupoFinanceiro grupo = repository.Find(id);

                if (grupo != null)
                {
                    grupo.Ativo = false;
                    grupo.AlteradoEm = DateTime.Now;
                    return repository.Alterar(grupo);
                }

                return grupo;
            }
        }

        public GrupoFinanceiro Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(GrupoFinanceiro item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.IdEmpresa == item.IdEmpresa && x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um grupo financeiro cadastrado com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<GrupoFinanceiro> Listar()
        {
            return repository.Listar();
        }
    }
}
