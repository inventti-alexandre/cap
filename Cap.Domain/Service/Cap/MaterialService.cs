using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class MaterialService : IBaseService<Material>
    {
        private IBaseRepository<Material> repository;

        public MaterialService()
        {
            repository = new EFRepository<Material>();
        }

        public Material Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Material material = repository.Find(id);

                if (material != null)
                {
                    material.Ativo = false;
                    material.AlteradoEm = DateTime.Now;
                    return repository.Alterar(material);
                }

                return material;
            }
        }

        public Material Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Material item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma material cadastrado com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Material> Listar()
        {
            return repository.Listar();
        }
    }
}
