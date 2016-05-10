using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Admin
{
    public class Feriado
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a data do feriado")]
        [Display(Name = "Data do feriado")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage ="Informe o nome do feriado")]
        [Display(Name = "Feriado")]
        [StringLength(40, ErrorMessage = "O feriado é composto por no máximo 40 caracteres")]
        public string Descricao { get; set; }

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
                //return new Avanhandava.Domain.Service.Admin.UsuarioService().Find(AlteradoPor);
                // TODO: retornar usuario
                return new Usuario();
            }
            set { }
        }
    }
}
