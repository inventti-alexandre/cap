using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Gen
{
    public class Indice
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Informe o índice")]
        [StringLength(20,ErrorMessage ="O índice é composto por no máximo 20 caracteres")]
        [Display(Name ="Índice")]
        public string Descricao { get; set; }

        [Required(ErrorMessage ="Descrição do índice")]
        [StringLength(100,ErrorMessage ="A descrição do índice é composta por no máximo 100 caracteres")]
        [Display(Name ="Descrição do índice")]
        public string Nome { get; set; }

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
