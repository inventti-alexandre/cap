using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Admin
{
    public class SistemaAreaService : IBaseService<SistemaArea>
    {
        private IBaseRepository<SistemaArea> repository;

        public SistemaAreaService()
        {
            repository = new EFRepository<SistemaArea>();
        }

        public SistemaArea Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                SistemaArea area = repository.Find(id);

                if (area != null)
                {
                    area.Ativo = false;
                    area.AlteradoEm = DateTime.Now;
                    repository.Alterar(area);
                }

                return area;
            }
        }

        public SistemaArea Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(SistemaArea item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma área cadastrada com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<SistemaArea> Listar()
        {
            return repository.Listar();
        }
    }
}
