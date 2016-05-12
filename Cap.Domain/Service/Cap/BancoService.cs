using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class BancoService : ILogin<Banco>
    {
        IBaseRepository<Banco> repository;

        public BancoService()
        {
            repository = new EFRepository<Banco>();
        }

        public Banco Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Banco banco = repository.Find(id);

                if (banco != null)
                {
                    banco.Ativo = false;
                    return repository.Alterar(banco);
                }

                return banco;
            }
        }

        public Banco Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Banco item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.Razao = item.Razao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um banco cadastrado com este nome fantasia");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Banco> Listar()
        {
            return repository.Listar();
        }
    }
}
