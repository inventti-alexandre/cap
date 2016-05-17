using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class MoedaService : IBaseService<Moeda>
    {
        IBaseRepository<Moeda> repository;

        public MoedaService()
        {
            repository = new EFRepository<Moeda>();
        }

        public Moeda Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Moeda moeda = repository.Find(id);

                if (moeda != null)
                {
                    moeda.Ativo = false;
                    moeda.AlteradoEm = DateTime.Now;
                    return repository.Alterar(moeda);
                }

                return moeda;
            }
        }

        public Moeda Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Moeda item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma moeda cadastrada com esta descrição");
            }

            if (item.Padrao == true)
            {
                if (repository.Listar().Where(x => x.IdEmpresa == item.IdEmpresa && x.Padrao == true && x.Id != item.Id).Count() > 0)
                {
                    throw new ArgumentException("Já existe uma moeda selecionada como padrão");
                }
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Moeda> Listar()
        {
            return repository.Listar();
        }
    }
}
