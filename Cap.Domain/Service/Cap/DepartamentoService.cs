using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Service.Cap
{
    public class DepartamentoService : IBaseService<Departamento>
    {
        private IBaseRepository<Departamento> repository;

        public DepartamentoService()
        {
            repository = new EFRepository<Departamento>();
        }

        public Departamento Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Departamento departamento = repository.Find(id);

                if (departamento != null)
                {
                    departamento.Ativo = false;
                    departamento.AlteradoEm = DateTime.Now;
                    return repository.Alterar(departamento);
                }

                return departamento;
            }
        }

        public Departamento Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Departamento item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.Endereco = item.Endereco.ToUpper().Trim();
            item.Bairro = item.Bairro.ToUpper().Trim();
            item.Cidade = item.Cidade.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id && x.IdEmpresa != item.IdEmpresa).Count() > 0)
            {
                throw new ArgumentException("Já existe um departamento cadastrado com este nome");
            }

            if (!string.IsNullOrEmpty(item.Cei))
            {
                if (repository.Listar().Where(x => x.Cei == item.Cei && x.Id != item.Id).Count() > 0)
                {
                    throw new ArgumentException("Já existe um departamento cadastrado com esta matrícula CEI");
                }
            }
            else
            {
                item.Cei = string.Empty;
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Departamento> Listar()
        {
            return repository.Listar();
        }
    }
}
