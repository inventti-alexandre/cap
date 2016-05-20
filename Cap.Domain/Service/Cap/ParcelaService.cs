using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class ParcelaService : IBaseService<Parcela>
    {
        IBaseRepository<Parcela> repository;

        public ParcelaService()
        {
            repository = new EFRepository<Parcela>();
        }

        public Parcela Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                var parcela = repository.Find(id);

                if (parcela != null)
                {
                    parcela.Ativo = false;
                    parcela.AlteradoEm = DateTime.Now;
                    return repository.Alterar(parcela);
                }

                return parcela;
            }
        }

        public Parcela Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Parcela item)
        {
            item.NN = (item.NN == null ? string.Empty : item.NN.ToUpper().Trim());
            item.NNData = (string.IsNullOrEmpty(item.NN) ? null : (item.NNData == null ? DateTime.Now : item.NNData));
            item.Observ = (item.Observ == null ? string.Empty : item.Observ.ToUpper().Trim());
            // TODO: verificar se ja foi pago => validar IdFPgto
            if (item.Liberado == true && item.LiberadoPor == null)
            {
                throw new ArgumentException("Não há responsável pela liberação");
            }
            item.CriadoEm = (item.CriadoEm == null ? DateTime.Now : item.CriadoEm);
            if (item.LibMaster == true && item.LibMasterPor == null)
            {
                throw new ArgumentException("Não há responsável pela liberação do master");
            }
            item.LiberadoEm = DateTime.Now;
            item.EmissaoPre = (item.EmissaoPre == DateTime.MinValue ? null : item.EmissaoPre);
            item.NNData = (item.NNData == DateTime.MinValue ? null : item.NNData);
            item.LiberadoEm = (item.LiberadoEm == DateTime.MinValue ? null : item.LiberadoEm);
            item.LibMasterEm = (item.LibMasterEm == DateTime.MinValue ? null : item.LibMasterEm);
            item.AlteradoEm = DateTime.Now;

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Parcela> Listar()
        {
            return repository.Listar();
        }
    }
}
