using Cap.Domain.Models.Admin;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cap.Domain.Models.Cap
{
    public class InfoCaixa
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Empresa")]
        [Range(1,double.MaxValue,ErrorMessage ="Empresa inválida")]
        public int EmpresaId { get; set; }

        [Display(Name ="Data caixa atual")]
        [Required(ErrorMessage ="Informe a data do caixa atual")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataCaixa { get; set; }

        [Display(Name ="Usuário")]
        [Range(1,double.MaxValue, ErrorMessage ="Usuário inválido")]
        public int UsuarioId { get; set; }

        [Display(Name ="Alterado em")]
        [Required]
        public DateTime AlteradoEm { get; set; }

        [Display(Name ="Usuário")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name ="Empresa")]
        public virtual Empresa Empresa { get; set; }
    }
}
