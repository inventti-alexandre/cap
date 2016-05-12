using Cap.Domain.Abstract;
using Cap.Domain.Models.Requisicao;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Requisicao
{
    public class MatGrupoService : ILogin<MatGrupo>
    {
        private IBaseRepository<MatGrupo> repository;

        public MatGrupoService()
        {
            repository = new EFRepository<MatGrupo>();
        }

        public MatGrupo Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                MatGrupo grupo = repository.Find(id);

                if (grupo != null)
                {
                    grupo.Ativo = false;
                    grupo.AlteradoEm = DateTime.Now;
                    return repository.Alterar(grupo);
                }

                return grupo;
            }
        }

        public MatGrupo Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(MatGrupo item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um grupo cadastrado com este nome");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<MatGrupo> Listar()
        {
            return repository.Listar();
        }
    }
}
