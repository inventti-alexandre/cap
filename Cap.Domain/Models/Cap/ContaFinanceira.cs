using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public enum TipoConta
    {
        [Display(Name = "Débito")]
        Debito = 0,
        [Display(Name = "Crédito")]
        Credito = 1
    }

    public class ContaFinanceira
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Empresa inválida")]
        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Empresa inválida")]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Grupo de lucro inválido")]
        [Display(Name = "Grupo de lucro")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione o grupo financeiro")]
        public int IdGrupoFinanceiro { get; set; }

        [Required(ErrorMessage = "Informe a descrição da conta financeira")]
        [Display(Name = "Grupo de custo")]
        [StringLength(40, ErrorMessage = "A descrição da conta financeira é composta por no máximo 60 caracteres")]
        public string Descricao { get; set; }

        [Display(Name ="Contabilizar esta conta nos relatórios?")]
        public bool Contabiliza { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de conta")]
        [Display(Name = "Tipo de conta")]
        [EnumDataType(typeof(TipoConta),ErrorMessage ="Tipo de conta inválido")]
        public TipoConta TipoConta { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name = "Usuário")]
        public virtual Usuario Usuario
        {
            get
            {
                return new UsuarioService().Find(AlteradoPor);
            }
        }

        [NotMapped]
        [Display(Name = "Empresa")]
        public virtual Empresa Empresa
        {
            get
            {
                return new EmpresaService().Find(IdEmpresa);
            }
        }

        [NotMapped]
        public virtual GrupoFinanceiro GrupoFinanceiro
        {
            get
            {
                return new GrupoFinanceiroService().Find(IdGrupoFinanceiro);
            }
        }
    }
}
