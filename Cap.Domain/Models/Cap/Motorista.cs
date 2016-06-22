using Cap.Domain.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Models.Cap
{
    public class Motorista
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Usuário")]
        [Range(1,double.MaxValue,ErrorMessage ="Selecione o usuário")]
        public int UsuarioId { get; set; }

        public bool Ativo { get; set; }

        [Display(Name ="Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [Display(Name ="Usuário")]
        public virtual Usuario Usuario { get; set; }
    }
}
