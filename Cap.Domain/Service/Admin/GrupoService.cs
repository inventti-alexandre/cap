using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Admin
{
    public class GrupoService : IBaseService<Grupo>
    {
        private IBaseRepository<Grupo> repository;

        public GrupoService()
        {
            repository = new EFRepository<Grupo>();
        }

        public Grupo Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Grupo grupo = repository.Find(id);

                if (grupo != null)
                {
                    grupo.Ativo = false;
                    grupo.AlteradoEm = DateTime.Now;
                    repository.Alterar(grupo);
                }

                return grupo;
            }
        }

        public Grupo Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Grupo item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.IdEmpresa == item.IdEmpresa && x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um grupo cadastrado com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Grupo> Listar()
        {
            return repository.Listar();
        }
    }
}
