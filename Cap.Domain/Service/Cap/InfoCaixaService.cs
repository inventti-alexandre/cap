using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class InfoCaixaService : IBaseService<InfoCaixa>
    {
        private IBaseRepository<InfoCaixa> repository;

        public InfoCaixaService()
        {
            repository = new EFRepository<InfoCaixa>();
        }

        public InfoCaixa Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Não foi possível excluir esta informação de caixa: " + e.Message);
            }
        }

        public InfoCaixa Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(InfoCaixa item)
        {
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.EmpresaId == item.EmpresaId && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existem informações de caixa gravadas para esta empresa");
            }

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<InfoCaixa> Listar()
        {
            return repository.Listar();
        }
    }
}
