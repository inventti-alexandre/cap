using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        [Range(1,double.MaxValue,ErrorMessage ="Selecione a empresa")]
        [Display(Name ="Empresa")]
        public int IdEmpresa { get; set; }

        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe o endereço")]
        [StringLength(100, ErrorMessage = "O endereço é composto por no máximo 100 caracteres")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Informe o bairro")]
        [StringLength(40, ErrorMessage = "O bairro é composto por no máximo 40 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        [StringLength(40, ErrorMessage = "A cidade é composta por no máximo 40 caracteres")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        [Range(0, double.MaxValue, ErrorMessage = "Selecione o Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        [Display(Name = "CEP")]
        [StringLength(8, ErrorMessage = "O CEP é composto por 8 caracteres", MinimumLength = 8)]
        public string Cep { get; set; }

        public string Cei { get; set; }

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
    }
}
