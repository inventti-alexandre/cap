using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class MotoristaService : IBaseService<Motorista>
    {
        private IBaseRepository<Motorista> repository;

        public MotoristaService()
        {
            repository = new EFRepository<Motorista>();
        }

        public Motorista Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Motorista motorista = repository.Find(id);

                if (motorista != null)
                {
                    motorista.Ativo = false;
                    return repository.Alterar(motorista);
                }

                return motorista;
            }
        }

        public Motorista Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Motorista item)
        {
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.UsuarioId == item.UsuarioId).Count() > 0)
            {
                throw new ArgumentException("Motorista já cadastrado");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Motorista> Listar()
        {
            return repository.Listar();
        }
    }
}
