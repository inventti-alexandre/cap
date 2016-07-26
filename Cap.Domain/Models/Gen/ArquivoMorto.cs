using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cap.Domain.Models.Gen
{
    public class ArquivoMorto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Empresa inválida")]
        [Display(Name ="Empresa")]
        [Range(1,double.MaxValue,ErrorMessage ="Empresa inválida")]
        public int EmpresaId { get; set; }

        [Required(ErrorMessage ="Informe o número da caixa")]
        [Display(Name ="Caixa número")]        
        [Range(1, double.MaxValue, ErrorMessage ="Informe o número da caixa")]
        public int Caixa { get; set; }

        [Required(ErrorMessage ="Selecione o departamento")]
        [Display(Name ="Departamento")]
        [Range(1,double.MaxValue,ErrorMessage ="Selecione o departamento")]
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage ="Informe o conteúdo da caixa")]
        [Display(Name ="Breve conteúdo da caixa")]
        [StringLength(200,ErrorMessage ="Máximo de 200 caracteres")]
        public string Conteudo { get; set; }

        [Display(Name ="Observações")]
        public string Observ { get; set; }

        [Display(Name ="Alterado por")]
        public int UsuarioId { get; set; }

        [Display(Name ="Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [Display(Name ="Alterado por")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name ="Empresa")]
        public virtual Empresa Empresa { get; set; }

        [Display(Name ="Departamento")]
        public virtual Departamento Departamento { get; set; }
    }
}
