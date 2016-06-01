using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class RegimeTributarioService : IBaseService<RegimeTributario>
    {
        private IBaseRepository<RegimeTributario> repository;

        public RegimeTributarioService()
        {
            repository = new EFRepository<RegimeTributario>();
        }

        public RegimeTributario Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                RegimeTributario regime = repository.Find(id);

                if (regime != null)
                {
                    regime.Ativo = false;
                    regime.AlteradoEm = DateTime.Now;
                    return repository.Alterar(regime);
                }

                return regime;
            }
        }

        public RegimeTributario Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(RegimeTributario item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um regime tributário cadastrado com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<RegimeTributario> Listar()
        {
            return repository.Listar();
        }
    }
}
