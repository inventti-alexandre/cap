using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Cap
{
    public class Banco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Informe o nome fantasia")]
        [Display(Name = "Nome fantasia")]
        [StringLength(40, ErrorMessage = "O nome fantasia é composto por no máximo 40 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a razão social")]
        [Display(Name = "Razão social")]
        [StringLength(100, ErrorMessage ="A razão social é composta por no máximo 100 caracteres")]
        public string Razao { get; set; }

        [Range(1,1000, ErrorMessage ="Informe o número do banco junta a Febraban")]
        public int NumFebraban { get; set; }

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
