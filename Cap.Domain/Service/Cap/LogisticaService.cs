using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class LogisticaService: IBaseService<Logistica>, ILogistica
    {
        private IBaseRepository<Logistica> repository;

        public LogisticaService()
        {
            repository = new EFRepository<Logistica>();
        }

        public void CancelarConclusao(int id)
        {
            try
            {
                var item = repository.Find(id);

                if (item == null)
                {
                    throw new ArgumentException("Serviço inexistente");
                }

                item.Concluido = false;
                item.ConcluidoEm = null;
                item.ConcluidoObserv = null;
                item.ConcluidoPor = null;

                if (item.DataServico < DateTime.Today.Date)
                {
                    item.DataServico = DateTime.Today.Date;
                }

                repository.Alterar(item);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public void Concluir(Logistica logistica)
        {
            try
            {
                if (logistica.Concluido == true)
                {
                    throw new ArgumentException($"Este serviço já estava concluído desde {logistica.ConcluidoEm}");
                }

                if (logistica.ConcluidoPor == null)
                {
                    throw new ArgumentException("Usuário inválido");
                }

                if (logistica.ConcluidoEm == null || logistica.ConcluidoEm >= DateTime.Today.Date.AddDays(1))
                {
                    logistica.ConcluidoEm = DateTime.Now;
                }

                logistica.Concluido = true;
                logistica.ConcluidoObserv = (logistica.ConcluidoObserv == null ? null : logistica.ConcluidoObserv.ToUpper().Trim());

                repository.Alterar(logistica);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public Logistica Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                var item = repository.Find(id);

                if (item != null)
                {
                    item.Ativo = false;
                    return repository.Alterar(item);
                }

                return item;
            }
        }

        public Logistica Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Logistica item)
        {
            item.Servico = item.Servico.ToUpper().Trim();
            item.Observ = (string.IsNullOrEmpty(item.Observ) ? string.Empty : item.Observ.ToUpper().Trim());
            item.ConcluidoObserv = (string.IsNullOrEmpty(item.ConcluidoObserv) ? string.Empty : item.ConcluidoObserv.ToUpper().Trim());
            item.AlteradoEm = DateTime.Now;
            if (item.DataServico < DateTime.Today.Date && item.Id == 0)
            {
                throw new ArgumentException("Data do serviço inválida");
            }

            if (item.Concluido == true)
            {
                item.ConcluidoEm = (item.ConcluidoEm == DateTime.MinValue ? DateTime.Now : item.ConcluidoEm > DateTime.Today.Date ? item.ConcluidoEm = DateTime.Now : item.ConcluidoEm);
                item.ConcluidoPor = (item.ConcluidoPor == null ? item.UsuarioId : item.ConcluidoPor);
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                item.Concluido = false;
                item.ConcluidoEm = null;
                item.ConcluidoPor = null;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Logistica> Listar()
        {
            return repository.Listar();
        }
    }
}
