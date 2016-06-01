using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class RegimeTributario
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Regime tributário")]
        [Required(ErrorMessage ="Informe o regime tributário")]
        [StringLength(40,ErrorMessage ="O regime tributário é composto por no máximo 40 caracteres")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Display(Name ="Alterado por")]
        public int AlteradoPor { get; set; }

        [Display(Name ="Alterado em")]
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
    }
}
