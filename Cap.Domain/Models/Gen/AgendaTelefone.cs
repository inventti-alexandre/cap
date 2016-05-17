using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Gen;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Gen
{
    public class AgendaTelefone
    {
        [Key]
        public int Id { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Nenhum vínculo com algum contato da agenda")]
        public int IdAgenda { get; set; }

        [Required(ErrorMessage ="Informe o número do telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Numero { get; set; }

        public string Contato { get; set; }

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
        public virtual Agenda Agenda
        {
            get
            {
                return new AgendaService().Find(IdAgenda);
            }
            set { }
        }
    }
}
