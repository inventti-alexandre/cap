using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Web.Areas.Erp.Models
{
    public class FornecedorModel
    {
        [Range(1, double.MaxValue, ErrorMessage = "Empresa inválida")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o nome fantasia")]
        [StringLength(40, ErrorMessage = "O nome fantasia é composto por no máximo 40 caracteres")]
        [Display(Name = "Nome fantasia")]
        public string Fantasia { get; set; }

        [StringLength(100, ErrorMessage = "A razão social é composta por no máximo 100 caracteres")]
        [Display(Name = "Razão social")]
        public string Razao { get; set; }

        [StringLength(40, ErrorMessage = "O contato é composto por no máximo 40 caracteres")]
        public string Contato { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Observações")]
        public string Observ { get; set; }

        [StringLength(14, ErrorMessage = "O CNPJ é composto por 14 caracteres")]
        public string CNPJ { get; set; }

        [StringLength(12, ErrorMessage = "A Inscrição Estadual é composta por 12 caracteres")]
        [Display(Name = "Inscrição Estadual")]
        public string IE { get; set; }

        [Display(Name = "Concessionária de serviços públicos")]
        public bool Concessionaria { get; set; }

        public bool Imposto { get; set; }

        [Display(Name = "Forma de pagamento")]
        public int IdPgto { get; set; }

        [Display(Name = "Endereço")]
        [StringLength(100, ErrorMessage = "O endereço é composto por no máximo 100 caracteres")]
        public string Endereco { get; set; }

        [StringLength(40, ErrorMessage = "O bairro é composto por no máximo 40 caracteres")]
        public string Bairro { get; set; }

        [StringLength(40, ErrorMessage = "A cidade é composta por no máximo 40 caracteres")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        [Range(0, double.MaxValue, ErrorMessage = "Selecione o Estado")]
        public int IdEstado { get; set; }

        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [StringLength(100, ErrorMessage = "O website é composto por no máximo 100 caracteres")]
        public string WebSite { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        public int IdAgenda { get; set; }

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
        [Display(Name = "Forma de pagamento")]
        public virtual Pgto FPgto
        {
            get
            {
                return new PgtoService().Find(IdPgto);
            }
        }

        [NotMapped]
        [Display(Name = "Usuário")]
        public virtual Usuario Usuario
        {
            get
            {
                return new UsuarioService().Find(AlteradoPor);
            }
            set { }
        }
    }
}