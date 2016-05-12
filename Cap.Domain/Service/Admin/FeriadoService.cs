using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Admin
{
    public class FeriadoService : ILogin<Feriado>
    {
        private IBaseRepository<Feriado> repository;

        public FeriadoService()
        {
            repository = new EFRepository<Feriado>();
        }

        public Feriado Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                throw new ArgumentException("Feriado inexistente");
            }
        }

        public Feriado Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Feriado item)
        {
            item.AlteradoEm = DateTime.Now;
            item.Descricao = item.Descricao.ToUpper().Trim();

            if (repository.Listar().Where(x => x.Data == item.Data && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um feriado cadastrado nesta data");
            }

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Feriado> Listar()
        {
            return repository.Listar();
        }
    }
}
