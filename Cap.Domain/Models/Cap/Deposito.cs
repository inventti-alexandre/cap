using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class Deposito
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Empresa inválida")]
        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Empresa inválida")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o nome do favorecido")]
        [StringLength(100, ErrorMessage ="O nome do favorecido é composto por no máximo 100 caracteres")]
        public string Favorecido { get; set; }

        [Display(Name ="Banco")]
        [Range(1,double.MaxValue,ErrorMessage ="Selecione o banco")]
        public int IdBanco { get; set; }

        [Required(ErrorMessage ="Informe o número da agência")]
        [StringLength(6,ErrorMessage ="O número da agência é composto por no máximo 6 caracteres")]
        [Display(Name = "Agência")]
        public string Agencia { get; set; }

        [Required(ErrorMessage ="Informe o número da conta")]
        [StringLength(15, ErrorMessage ="O número da conta é composto por no máximo 15 caracteres")]
        public string Conta { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(14,ErrorMessage ="O número do CPF/CNPJ é composto por no máximo 14 caracteres")]
        [Display(Name ="CPF/CNPJ")]
        public string Cpf { get; set; }

        [Display(Name = "Conta poupança")]
        public bool Poupanca { get; set; }

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Observações")]
        [StringLength(100,ErrorMessage ="A observação é composta por no máximo 100 caracteres")]
        public string Observ { get; set; }

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
            set { }
        }

        [NotMapped]
        [Display(Name = "Empresa")]
        public virtual Empresa Empresa
        {
            get
            {
                return new EmpresaService().Find(IdEmpresa);
            }
            set { }
        }

        [NotMapped]
        public virtual Banco Banco
        {
            get
            {
                return new BancoService().Find(IdBanco);
            }
            set { }
        }
    }
}
