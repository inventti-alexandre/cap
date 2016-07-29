using Cap.Domain.Abstract;
using Cap.Domain.Abstract.Cap;
using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Respository;
using Cap.Domain.Service.Admin;
using System;
using System.Linq;

namespace Cap.Domain.Service.Cap
{
    public class InfoCaixaService : IBaseService<InfoCaixa>, IInfoCaixa
    {
        private IBaseRepository<InfoCaixa> repository;
        private IBaseService<Feriado> feriado;

        public InfoCaixaService()
        {
            repository = new EFRepository<InfoCaixa>();
            feriado = new FeriadoService();
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

        public InfoCaixa GetInfoCaixa(int idEmpresa, int idUsuario)
        {
            InfoCaixa item = repository.Listar().Where(x => x.EmpresaId == idEmpresa).FirstOrDefault();

            if (item == null)
            {
                item = new InfoCaixa
                {
                    AlteradoEm = DateTime.Now,
                    DataCaixa = DateTime.Today.Date,
                    DataProximoCaixa = getDataProximoCaixa(idEmpresa),
                    DataUltimoCaixa = getDataUltimoCaixa(idEmpresa),
                    EmpresaId = idEmpresa,
                    UsuarioId = idUsuario
                };
                repository.Incluir(item);
            }

            return item;
        }

        public int Gravar(InfoCaixa item)
        {
            try
            {
                if (item.DataUltimoCaixa >= item.DataCaixa)
                {
                    throw new ArgumentException("Data do último caixa maior ou igual a data do caixa atual");
                }

                if (item.DataProximoCaixa <= item.DataCaixa)
                {
                    throw new ArgumentException("Data do próximo caixa menor ou igual a data do caixa atual");
                }

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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<InfoCaixa> Listar()
        {
            return repository.Listar();
        }


        #region [ privates ]

        private DateTime getDataUltimoCaixa(int idEmpresa)
        {
            DateTime d = DateTime.Today.Date.AddDays(-1);

            if (feriado.Listar().Where(x => x.IdEmpresa == idEmpresa && x.Data == d).Count() > 0)
            {
                d.AddDays(-1);
            }

            if (d.DayOfWeek == DayOfWeek.Sunday)
            {
                d = d.AddDays(-2);
            }

            if (d.DayOfWeek == DayOfWeek.Saturday)
            {
                d = d.AddDays(-1);
            }

            if (feriado.Listar().Where(x => x.IdEmpresa == idEmpresa && x.Data == d).Count() > 0)
            {
                d.AddDays(-1);
            }

            return d;
        }

        private DateTime getDataProximoCaixa(int idEmpresa)
        {
            DateTime d = DateTime.Today.Date.AddDays(1);

            if (feriado.Listar().Where(x => x.IdEmpresa == idEmpresa && x.Data == d).Count() > 0)
            {
                d.AddDays(1);
            }

            if (d.DayOfWeek == DayOfWeek.Sunday)
            {
                d = d.AddDays(2);
            }

            if (d.DayOfWeek == DayOfWeek.Saturday)
            {
                d = d.AddDays(1);
            }

            if (feriado.Listar().Where(x => x.IdEmpresa == idEmpresa && x.Data == d).Count() > 0)
            {
                d.AddDays(1);
            }

            return d;
        }

        #endregion
    }
}
