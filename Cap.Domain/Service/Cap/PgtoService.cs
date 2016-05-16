using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class PgtoService : IBaseService<Pgto>
    {
        IBaseRepository<Pgto> repository;

        public PgtoService()
        {
            repository = new EFRepository<Pgto>();
        }

        public Pgto Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Pgto pgto = repository.Find(id);

                if (pgto != null)
                {
                    pgto.Ativo = false;
                    pgto.AlteradoEm = DateTime.Now;
                    return repository.Alterar(pgto);
                }

                return pgto;
            }
        }

        public Pgto Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Pgto item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um pagamento cadastrado com esta descrição");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Pgto> Listar()
        {
            return repository.Listar();
        }
    }
}
