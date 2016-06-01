using Cap.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Models.Admin
{
    public class SistemaTela
    {
        [Key]
        public int Id { get; set; }

        public string TextoMenu { get; set; }

        public string Link { get; set; }

        [Required(ErrorMessage ="Informe uma breve descrição sobre a tela do sistema")]
        public string Descricao { get; set; }

        public int IdArea { get; set; }

        public int IdSistemaRegra { get; set; }

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
