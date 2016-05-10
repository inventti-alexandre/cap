using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Cap.Domain.Respository
{
    public class EFDbContext: System.Data.Entity.DbContext 
    {
        public EFDbContext()
            : base("CapConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<CliGrupoPermissao>().HasKey(x => new { x.IdGrupo, x.IdPermissao });
        }

        public DbSet<Banco> Banco { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<EstadoCivil> EstadoCivil { get; set; }
        public DbSet<Feriado> Feriado { get; set; }
        public DbSet<FPgto> FPgto { get; set; }
        public DbSet<Pgto> Pgto { get; set; }
        public DbSet<SistemaParametro> SistemaParametro { get; set; }
        public DbSet<Socio> Socio { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
