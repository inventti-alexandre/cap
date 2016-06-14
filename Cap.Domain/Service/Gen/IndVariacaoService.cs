using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Gen;
using Cap.Domain.Models.Gen;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Gen
{
    public class IndVariacaoService : IBaseService<IndVariacao>, IIndVariacaoCalculo
    {
        private IBaseRepository<IndVariacao> repository;

        public IndVariacaoService()
        {
            repository = new EFRepository<IndVariacao>();
        }

        public decimal CalcularVariacao(int idIndice, DateTime inicial, DateTime final)
        {
            // variacoes para o indice no periodo
            var variacoes = repository.Listar()
                .Where(x => x.IdIndice == idIndice
                && x.DataVariacao >= inicial && x.DataVariacao <= final)
                .ToList();

            // calculo do acumulado
            decimal acumulado = 1;

            foreach (var item in variacoes)
            {
                acumulado = (acumulado * (1 + (item.Variacao / 100)));
            }

            return ((acumulado - 1) * 100);
        }

        public IndVariacao Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                throw new ArgumentException("Índice inexistente");
            }
        }

        public IndVariacao Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(IndVariacao item)
        {
            item.AlteradoEm = DateTime.Now;
            if (item.DataVariacao == DateTime.MinValue || item.DataVariacao > DateTime.Today.Date.AddMonths(1))
            {
                throw new ArgumentException("Data base inválida");
            }
            item.DataVariacao = new DateTime(item.DataVariacao.Year, item.DataVariacao.Month, 1);

            if (repository.Listar().Where(x => x.DataVariacao == item.DataVariacao && x.IdIndice == item.IdIndice && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Índice já cadastrado");
            }

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<IndVariacao> Listar()
        {
            return repository.Listar();
        }
    }
}
