using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class GrupoLucroService: IBaseService<GrupoLucro>
    {
        IBaseRepository<GrupoLucro> repository;

        public GrupoLucroService()
        {
            repository = new EFRepository<GrupoLucro>();
        }

        public GrupoLucro Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                GrupoLucro grupo = repository.Find(id);

                if (grupo != null)
                {
                    grupo.Ativo = false;
                    grupo.AlteradoEm = DateTime.Now;
                    return repository.Alterar(grupo);
                }

                return grupo;
            }
        }

        public GrupoLucro Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(GrupoLucro item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Grupo de lucro já cadastrado");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<GrupoLucro> Listar()
        {
            return repository.Listar();
        }
    }
}
