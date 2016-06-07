using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Admin;
using Cap.Domain.Models.Admin;
using Cap.Domain.Respository;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Cap.Domain.Service.Admin
{
    public class TelaRegraService : IBaseService<TelaRegra>, ITelaRegra
    {
        private IBaseRepository<TelaRegra> repository;
        private IBaseRepository<SistemaRegra> sistemaRegraRepository;

        public TelaRegraService()
        {
            repository = new EFRepository<TelaRegra>();
            sistemaRegraRepository = new EFRepository<SistemaRegra>();
        }

        public TelaRegra Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public TelaRegra Find(int id)
        {
            return repository.Find(id);
        }

        public IEnumerable<TelaRegraModel> GetRegras(int idTela)
        {
            var regras = sistemaRegraRepository.Listar().Where(x => x.Ativo == true).ToList().OrderBy(x => x.Descricao);

            var regrasSelecionadas = repository.Listar().Where(x => x.IdTela == idTela);

            var lista = new List<TelaRegraModel>();
            foreach (var item in regras)
            {
                lista.Add(new TelaRegraModel { IdRegra = item.Id, IdTela = idTela, Selecionado = regrasSelecionadas.Where(x => x.IdRegra == item.Id).Count() > 0 });
            }

            // retorna lista com IdTela,IdRegra,Selecionado
            return lista;
        }

        public int Gravar(TelaRegra item)
        {
            if (repository.Listar().Where(x => x.IdRegra == item.IdRegra && x.IdTela == item.IdTela && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Tela/regra já cadastrados anteriormente");
            }

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<TelaRegra> Listar()
        {
            return repository.Listar();
        }

        public void SetRegras(int idTela, int[] idRegras)
        {
            EFDbContext ctx = new EFDbContext();

            var regrasExistentes = ctx.TelaRegra.Where(x => x.IdTela == idTela).ToList();
            if (regrasExistentes.Count > 0)
            {
                ctx.TelaRegra.RemoveRange(regrasExistentes);
                ctx.SaveChanges();
            }

            // grava novas regras
            if (idRegras != null)
            {
                foreach (var item in idRegras)
                {
                    repository.Incluir(new TelaRegra { IdRegra = item, IdTela = idTela });
                }
            }
        }
    }
}
