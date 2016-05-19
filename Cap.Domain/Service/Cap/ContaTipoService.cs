using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class ContaTipoService: IBaseService<Models.Cap.ContaTipo>
    {
        IBaseRepository<Models.Cap.ContaTipo> repository;

        public ContaTipoService()
        {
            repository = new EFRepository<Models.Cap.ContaTipo>();
        }

        public ContaTipo Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Models.Cap.ContaTipo tipo = repository.Find(id);

                if (tipo != null)
                {
                    tipo.Ativo = false;
                    tipo.AlteradoEm = DateTime.Now;
                    return repository.Alterar(tipo);
                }

                return tipo;
            }
        }

        public ContaTipo Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Models.Cap.ContaTipo item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um tipo de conta cadastrado com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Models.Cap.ContaTipo> Listar()
        {
            return repository.Listar();
        }
    }
}
