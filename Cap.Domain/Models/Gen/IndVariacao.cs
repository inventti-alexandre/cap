using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Gen;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Gen
{
    public class IndVariacao
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Índice")]
        public int IdIndice { get; set; }

        [Required(ErrorMessage ="Informe a data base")]
        [Display(Name ="Data base")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataVariacao { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public decimal Variacao { get; set; }

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

        [NotMapped]
        [Display(Name ="Índice")]
        public virtual Indice Indice
        {
            get
            {
                return new IndiceService().Find(IdIndice);
            }
        }
    }
}
