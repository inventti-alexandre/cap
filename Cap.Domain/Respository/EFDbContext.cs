using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Models.Email;
using Cap.Domain.Models.Gen;
using Cap.Domain.Models.Requisicao;
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
            modelBuilder.Entity<IndVariacao>().Property(x => x.Variacao).HasPrecision(12, 2);

            //modelBuilder.Entity<CliGrupoPermissao>().HasKey(x => new { x.IdGrupo, x.IdPermissao });
        }

        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<AgendaEmail> AgendaEmail { get; set; }
        public DbSet<AgendaTelefone> AgendaTelefone { get; set; }
        public DbSet<ArquivoMorto> ArquivoMorto { get; set; }
        public DbSet<Banco> Banco { get; set; }
        public DbSet<CentroCusto> CentroCusto { get; set; }
        public DbSet<CentroLucro> CentroLucro { get; set; }
        public DbSet<Conta> Conta { get; set; }
        public DbSet<ContaFinanceira> ContaFinanceira { get; set; }
        public DbSet<ContaTipo> ContaTipo { get; set; }
        public DbSet<CotCotacao> CotCotacao { get; set; }
        public DbSet<CotCotadoCom> CotCotadoCom { get; set; }
        public DbSet<CotDadosCotacao> CotDadosCotacao { get; set; }
        public DbSet<CotFornecedor> CotFornecedor { get; set; }
        public DbSet<CotGrupo> CotGrupo { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Deposito> Deposito { get; set; }
        public DbSet<EmailConfig> EmailConfig { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<EstadoCivil> EstadoCivil { get; set; }
        public DbSet<Feriado> Feriado { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<FPgto> FPgto { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<GrupoCusto> GrupoCusto { get; set; }
        public DbSet<GrupoFinanceiro> GrupoFinanceiro { get; set; }
        public DbSet<GrupoLucro> GrupoLucro { get; set; }
        public DbSet<Indice> Indice { get; set; }
        public DbSet<IndVariacao> IndVariacao { get; set; }
        public DbSet<Logistica> Logistica { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<MatGrupo> MatGrupo { get; set; }
        public DbSet<Moeda> Moeda { get; set; }
        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Parcela> Parcela { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Pgto> Pgto { get; set; }
        public DbSet<RegimeTributario> RegimeTributario { get; set; }
        public DbSet<ReqAutorizante> ReqAutorizante { get; set; }
        public DbSet<ReqMaterial> ReqMaterial { get; set; }
        public DbSet<ReqRequisicao> ReqRequisicao { get; set; }
        public DbSet<SistemaArea> SistemaArea { get; set; }
        public DbSet<SistemaParametro> SistemaParametro { get; set; }
        public DbSet<SistemaRegra> SistemaRegra { get; set; }
        public DbSet<SistemaTela> SistemaTela { get; set; }
        public DbSet<Socio> Socio { get; set; }
        public DbSet<TelaRegra> TelaRegra { get; set; }
        public DbSet<Unidade> Unidade { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
