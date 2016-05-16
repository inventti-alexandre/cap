using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Informe o nome fantasia")]
        [Display(Name ="Nome fantasia")]     
        [StringLength(40,ErrorMessage ="O nome fantasia é composto por no máximo 40 caracteres")]   
        public string Fantasia { get; set; }

        [Required(ErrorMessage ="Informe a razão social")]
        [Display(Name ="Razão social")]
        [StringLength(100,ErrorMessage ="A razão social é composta por no máximo 100 caracteres")]
        public string Razao { get; set; }

        [Required(ErrorMessage ="Informe o CNPJ")]
        [Display(Name ="CNPJ")]
        [StringLength(14,ErrorMessage ="O CNPJ é composto por 14 números", MinimumLength =14)]
        public string Cnpj { get; set; }

        [Display(Name ="Inscrição Estadual")]
        [StringLength(12, ErrorMessage ="A Inscrição Estadual é composta por no máximo 12 caracteres")]
        public string IE { get; set; }

        [Required(ErrorMessage ="Informe o endereço")]
        [StringLength(100, ErrorMessage ="O endereço é composto por no máximo 100 caracteres")]
        public string Endereco { get; set; }

        [Required(ErrorMessage ="Informe o bairro")]
        [StringLength(40,ErrorMessage ="O bairro é composto por no máximo 40 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage ="Informe a cidade")]
        [StringLength(40, ErrorMessage = "A cidade é composta por no máximo 40 caracteres")]
        public string Cidade { get; set; }

        [Display(Name ="Estado")]
        [Range(0,double.MaxValue,ErrorMessage ="Selecione o Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage ="Informe o CEP")]
        [Display(Name ="CEP")]
        [StringLength(8, ErrorMessage = "O CEP é composto por 8 caracteres", MinimumLength =8)]
        public string Cep { get; set; }

        [Required(ErrorMessage ="Informe o telefone")]
        [StringLength(20, ErrorMessage = "O telefone é composto por no máximo 20 caracteres")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage ="Informe o email")]
        [StringLength(100, ErrorMessage = "O email é composto por no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Possui e-CNPJ?")]
        public bool ECnpj { get; set; }

        [Display(Name = "Vencimento do e-CNPJ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ECnpjVencto { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name = "Estado")]
        public virtual Estado Estado
        {
            get
            {
                return new EstadoService().Find(IdEstado);
            }
            set { }
        }
    }
}
