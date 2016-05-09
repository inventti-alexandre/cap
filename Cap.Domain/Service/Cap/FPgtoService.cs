using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class FPgtoService : IBaseService<FPgto>
    {
        IBaseRepository<FPgto> repository;

        public FPgtoService()
        {
            repository = new EFRepository<FPgto>();
        }

        public FPgto Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                var fpgto = repository.Find(id);

                if (fpgto != null)
                {
                    fpgto.Ativo = false;
                    return repository.Alterar(fpgto);
                }

                return fpgto;
            }
        }

        public FPgto Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(FPgto item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma forma de pagamento cadastrada com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<FPgto> Listar()
        {
            return repository.Listar();
        }
    }
}
