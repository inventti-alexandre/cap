using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class CentroLucroService : IBaseService<CentroLucro>
    {
        IBaseRepository<CentroLucro> repository;

        public CentroLucroService()
        {
            repository = new EFRepository<CentroLucro>();
        }

        public CentroLucro Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                CentroLucro centro = repository.Find(id);

                if (centro != null)
                {
                    centro.Ativo = false;
                    centro.AlteradoEm = DateTime.Now;
                    return repository.Alterar(centro);
                }

                return centro;
            }
        }

        public CentroLucro Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(CentroLucro item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um centro de lucro cadastrado com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<CentroLucro> Listar()
        {
            return repository.Listar();
        }
    }
}
