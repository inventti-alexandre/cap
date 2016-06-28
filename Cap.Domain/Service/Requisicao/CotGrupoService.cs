using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class CotGrupoService : IBaseService<CotGrupo>
    {
        private IBaseRepository<CotGrupo> repository;

        public CotGrupoService()
        {
            repository = new EFRepository<CotGrupo>();
        }

        public CotGrupo Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                var item = repository.Find(id);

                if (item != null)
                {
                    item.Ativo = false;
                    item.AlteradoEm = DateTime.Now;
                    return repository.Alterar(item);
                }

                return item;
            }
        }

        public CotGrupo Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(CotGrupo item)
        {
            item.AlteradoEm = DateTime.Now;
            item.Descricao = item.Descricao.ToUpper().Trim();

            if (repository.Listar().Where(x => x.EmpresaId == item.EmpresaId && x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um grupo de cotação cadastrado com este nome");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<CotGrupo> Listar()
        {
            return repository.Listar();
        }
    }
}
