using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class ContaTipo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Empresa inválida")]
        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Empresa inválida")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o tipo da conta")]
        [Display(Name = "Tipo da conta")]
        [StringLength(40, ErrorMessage = "O tipo da conta é composto por no máximo 40 caracteres")]
        public string Descricao { get; set; }

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
    }
}
