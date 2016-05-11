using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class MatGrupo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Informe o nome do grupo")]
        [StringLength(40, ErrorMessage ="O nome do grupo é composto por no máximo 40 caracteres")]
        [Display(Name ="Grupo")]
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
    }
}
