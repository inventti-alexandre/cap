using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class EmpresaService : ILogin<Empresa>
    {
        private IBaseRepository<Empresa> repository;

        public EmpresaService()
        {
            repository = new EFRepository<Empresa>();
        }

        public Empresa Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Empresa empresa = repository.Find(id);

                if (empresa != null)
                {
                    empresa.Ativo = false;
                    empresa.AlteradoEm = DateTime.Now;
                    return repository.Alterar(empresa);
                }

                return empresa;
            }
        }

        public Empresa Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Empresa item)
        {
            item.AlteradoEm = DateTime.Now;
            item.Bairro = item.Bairro.ToUpper().Trim();
            item.Cidade = item.Cidade.ToUpper().Trim();
            item.Email = item.Email.ToLower().Trim();
            item.Fantasia = item.Fantasia.ToUpper().Trim();
            item.Razao = item.Razao.ToUpper().Trim();

            if (repository.Listar().Where(x => x.Fantasia == item.Fantasia && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma empresa cadastrada com este nome fantasia");
            }

            if (repository.Listar().Where(x => x.Cnpj == item.Cnpj && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma empresa cadastrada com este CNPJ");
            }

            if (item.ECnpj == true && item.ECnpjVencto == null)
            {
                throw new ArgumentException("Informe a data de vencimento do e-CNPJ");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Empresa> Listar()
        {
            return repository.Listar();
        }
    }
}
