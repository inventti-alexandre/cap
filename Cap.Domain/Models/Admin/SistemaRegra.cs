using Cap.Domain.Service.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Admin
{
    public class SistemaRegra
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a área")]
        [Display(Name = "Regra")]
        [StringLength(100, ErrorMessage = "A regra é composta por no máximo 100 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o sufixo")]
        [Display(Name ="Sufixo")]
        [StringLength(3,ErrorMessage ="O sufixo é composto por no máximo 100 caracteres")]
        public string Sufixo { get; set; }

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
        }
    }
}
