using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class Socio
    {
        [Key]
        public int Id { get; set; }

        [Range(1,double.MaxValue, ErrorMessage ="Selecione a empresa")]
        public int IdEmpresa { get; set; }

        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        [StringLength(100, ErrorMessage = "O endereço é composto por no máximo 100 caracteres")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Informe o bairro")]
        [StringLength(40, ErrorMessage = "O bairro é composto por no máximo 40 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        [StringLength(40, ErrorMessage = "A cidade é composta por no máximo 40 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Selecione o Estado")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        [Display(Name = "CEP")]
        [StringLength(8, ErrorMessage = "O CEP é composto por 8 caracteres", MinimumLength = 8)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe o telefone")]
        [StringLength(20, ErrorMessage = "O telefone é composto por no máximo 20 caracteres")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o email")]
        [StringLength(100, ErrorMessage = "O email é composto por no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Informe o CPF")]
        [StringLength(11, ErrorMessage ="O CPF é composto por no máximo 11 caracteres", MinimumLength =11)]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage ="Informe a data de nascimento")]
        [Display(Name ="Data de nascimento")]
        public DateTime Nascimento { get; set; }

        [StringLength(100, ErrorMessage ="O nome do conjuge é composto por no máximo 100 caracteres")]
        public string Conjuge { get; set; }

        [Required(ErrorMessage ="Informe a profissão")]
        [StringLength(40,ErrorMessage ="A profissão é composta por no máximo 40 caracteres")]
        [Display(Name ="Profissão")]
        public string Profissao { get; set; }

        [Required(ErrorMessage = "Informe a nacionalidade")]
        [StringLength(40, ErrorMessage = "A nacionalidade é composta por no máximo 40 caracteres")]
        public string Nacionalidade { get; set; }

        [Required(ErrorMessage ="Selecione o estado civil")]
        [Range(1,double.MaxValue,ErrorMessage ="Selecione o estado civil")]
        [Display(Name = "Estado civil")]
        public int IdEstadoCivil { get; set; }

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
        [Display(Name = "Estado")]
        public virtual Estado Estado
        {
            get
            {
                return new EstadoService().Find(IdEstado);
            }
            set { }
        }

        [NotMapped]
        public virtual Empresa Empresa
        {
            get
            {
                return new EmpresaService().Find(IdEmpresa);
            }
            set { }
        }

        [NotMapped]
        public virtual EstadoCivil EstadoCivil
        {
            get
            {
                return new EstadoCivilService().Find(IdEstadoCivil);
            }
            set { }
        }
    }
}
