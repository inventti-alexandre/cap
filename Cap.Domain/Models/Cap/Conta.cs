using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class Conta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Empresa inválida")]
        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Empresa inválida")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o nome da conta")]
        [Display(Name = "Nome da conta")]
        [StringLength(40, ErrorMessage = "O nome da conta é composto por no máximo 40 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o valor do saldo anterior")]
        [Display(Name = "Saldo anterior")]
        public decimal SaldoAnterior { get; set; }

        [Required(ErrorMessage ="Informe a data do saldo anterior")]
        [Display(Name = "Data saldo anterior")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataSaldoAnterior { get; set; }

        [Required(ErrorMessage = "Informe o valor do saldo atual")]
        [Display(Name = "Saldo atual")]
        public decimal Saldo { get; set; }

        [Required(ErrorMessage = "Informe a data do saldo")]
        [Display(Name = "Data saldo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataSaldo { get; set; }

        [Display(Name ="Cheque atual")]
        [Range(0, double.MaxValue, ErrorMessage ="O número do cheque atual não pode ser menor ou igual a zero")]
        public int ChequeAtual { get; set; }

        [Range(1,double.MaxValue, ErrorMessage ="Selecione o banco")]
        [Display(Name = "Banco")]
        public int IdBanco { get; set; }

        [Required(ErrorMessage ="Informe o número da agência")]
        [Display(Name ="Agência")]
        [StringLength(6, ErrorMessage ="O número da agência é composto por no máximo 6 caracteres")]
        public string Agencia { get; set; }

        [Required(ErrorMessage = "Informe o nome da agência")]
        [Display(Name = "Nome da agência")]
        [StringLength(60, ErrorMessage = "O nome da agência é composto por no máximo 60 caracteres")]
        public string AgenciaNome { get; set; }


        [Required(ErrorMessage = "Informe o número da conta")]
        [Display(Name ="Conta")]
        [StringLength(15,ErrorMessage ="O número da conta é composto por no máximo 15 caracteres")]
        public string ContaNumero { get; set; }

        [Display(Name = "Observações")]
        public string Observ { get; set; }

        [Display(Name ="Tipo da conta")]
        [Range(1,double.MaxValue, ErrorMessage ="Selecione o tipo da conta")]
        public int IdContaTipo { get; set; }

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
        public virtual Banco Banco
        {
            get
            {
                return new BancoService().Find(IdBanco);
            }
        }

        [NotMapped]
        public virtual ContaTipo ContaTipo
        {
            get
            {
                return new ContaTipoService().Find(IdContaTipo);
            }
        }
    }
}
