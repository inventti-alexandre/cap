using Cap.Domain.Abstract;
using Cap.Domain.Models.Admin;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Admin
{
    public class SistemaTelaService: IBaseService<SistemaTela>
    {
        private IBaseRepository<SistemaTela> repository;

        public SistemaTelaService()
        {
            this.repository = new EFRepository<SistemaTela>();
        }

        public SistemaTela Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                SistemaTela tela = repository.Find(id);

                if (tela != null)
                {
                    tela.Ativo = false;
                    tela.AlteradoEm = DateTime.Now;
                    repository.Alterar(tela);
                }

                return tela;
            }
        }

        public SistemaTela Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(SistemaTela item)
        {
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.TextoMenu = item.TextoMenu.Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.TextoMenu == item.TextoMenu && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma tela cadastrada com esta descrição");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<SistemaTela> Listar()
        {
            return repository.Listar();
        }
    }
}
