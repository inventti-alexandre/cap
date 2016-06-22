using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Models.Cap
{
    public class Logistica
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Motorista")]
        [Range(1,double.MaxValue,ErrorMessage ="Selecione o motorista")]
        public int MotoristaId { get; set; }

        [Display(Name ="Data")]
        [Required(ErrorMessage ="Informe a data do serviço")]
        public DateTime DataServico { get; set; }

        [Display(Name ="Serviço")]
        [Required(ErrorMessage ="Informe o serviço")]
        public string Servico { get; set; }

        [Display(Name ="Observações")]
        public string Observ { get; set; }

        [Display(Name ="Alterado por")]
        [Required(ErrorMessage ="Usuário inválido")]
        public int UsuarioId { get; set; }

        [Display(Name ="Alterado em")]
        [Required]
        public DateTime AlteradoEm { get; set; }

        public bool Ativo { get; set; }

        [Display(Name ="Serviço concluído?")]
        public bool Concluido { get; set; }

        [Display(Name ="Concluído em")]
        public DateTime? ConcluidoEm { get; set; }

        [Display(Name ="Concluído por")]
        public int? ConcluidoPor { get; set; }

        [Display(Name ="Observações")]
        public string ConcluidoObserv { get; set; }
    }
}
