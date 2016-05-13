using Cap.Domain.Abstract;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class SocioService : IBaseService<Socio>
    {
        private IBaseRepository<Socio> repository;

        public SocioService()
        {
            repository = new EFRepository<Socio>();
        }

        public Socio Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                Socio socio = repository.Find(id);

                if (socio != null)
                {
                    socio.Ativo = false;
                    socio.AlteradoEm = DateTime.Now;
                    return repository.Alterar(socio);
                }

                return socio;
            }
        }

        public Socio Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(Socio item)
        {
            item.AlteradoEm = DateTime.Now;
            item.Bairro = item.Bairro.ToUpper().Trim();
            item.Cidade = item.Cidade.ToUpper().Trim();
            item.Conjuge = string.IsNullOrEmpty(item.Conjuge) ? string.Empty : item.Conjuge.ToUpper().Trim();
            item.Email = item.Email.ToLower().Trim();
            item.Endereco = item.Endereco.ToUpper().Trim();
            if (item.Nascimento > DateTime.Today.Date.AddYears(-18))
            {
                throw new ArgumentException("Data de nascimento inválida");
            }
            item.Nacionalidade = item.Nacionalidade.ToUpper().Trim();
            item.Nome = item.Nome.ToUpper().Trim();
            item.Profissao = item.Profissao.ToUpper().Trim();

            if (repository.Listar().Where(x => x.Nome == item.Nome && x.Id != item.Id && x.IdEmpresa == item.IdEmpresa).Count() > 0)
            {
                throw new ArgumentException("Sócio já cadastrado nesta empresa");
            }

            if (repository.Listar().Where(x => x.Cpf == item.Cpf && x.Id != item.Id && x.IdEmpresa == item.IdEmpresa).Count() > 0)
            {
                throw new ArgumentException("Já existe um sócio cadastrado com este CPF");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<Socio> Listar()
        {
            return repository.Listar();
        }
    }
}
