using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class CentroCustoService: IBaseService<CentroCusto>
    {
        IBaseRepository<CentroCusto> repository;

        public CentroCustoService()
        {
            repository = new EFRepository<CentroCusto>();
        }

        public CentroCusto Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                CentroCusto centro = repository.Find(id);

                if (centro != null)
                {
                    centro.Ativo = false;
                    centro.AlteradoEm = DateTime.Now;
                    return repository.Alterar(centro);
                }

                return centro;
            }
        }

        public CentroCusto Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(CentroCusto item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um centro de custo cadastrado com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<CentroCusto> Listar()
        {
            return repository.Listar();
        }
    }
}
