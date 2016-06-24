using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap.Domain.Models.Requisicao
{
    public enum Situacao
    {
        EmDigitacao = 0,
        Cotar = 1,
        EmCotacao = 2,
        Cotado = 3,
        Cancelada = 4,
        Aprovada = 5,
        Comprada = 6,
        Entregue = 7
    }

    public class ReqRequisicao
    {
        [Key]
        public int Id { get; set; }

        [Range(1,double.MaxValue,ErrorMessage ="Selecione o departamento")]
        [Display(Name ="Departamento")]
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage ="Informe a data de solicitação da requisição")]
        [Display(Name ="Solicitado em")]
        public DateTime SolicitadoEm { get; set; }

        [Range(1,double.MaxValue, ErrorMessage ="Requisitante inválido")]
        [Display(Name ="Solicitado por")]
        public int IdSolicitadoPor { get; set; }

        [Required(ErrorMessage ="Informe a data máxima para cotação")]
        [Display(Name = "Cotar até")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime CotarAte { get; set; }

        [Required(ErrorMessage ="Informe a data de entrega")]
        [Display(Name ="Entregar dia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime EntregarDia { get; set; }

        [Display(Name ="Observações")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

        [Display(Name = "Entrega noturna")]
        public bool EntregaNoturna { get; set; }

        public Situacao Situacao { get; set; }

        [Display(Name ="Cotado por")]
        public int IdCotadoPor { get; set; }

        [Display(Name = "Cotado em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime? CotadoEm { get; set; }

        [Display(Name = "Liberado para compra")]
        public bool LiberadoParaCompra { get; set; }

        [Display(Name = "Liberado em")]
        public DateTime? LiberadoEm { get; set; }

        [Display(Name = "Liberado por")]
        public int IdLiberadoPor { get; set; }

        [Display(Name ="Observações da liberação")]
        [DataType(DataType.MultilineText)]
        public string LiberadoObserv { get; set; }

        [NotMapped]
        public virtual Departamento Departamento
        {
            get
            {
                return new DepartamentoService().Find(IdDepartamento);
            }
            set { }
        }

        [NotMapped]
        public virtual Usuario SolicitadoPor
        {
            get
            {
                return new UsuarioService().Find(IdSolicitadoPor);
            }
            set { }
        }

        [NotMapped]
        public virtual Usuario CotadoPor
        {
            get
            {
                return new UsuarioService().Find(IdCotadoPor);
            }
            set { }
        }

        [NotMapped]
        public virtual Usuario LiberadoPor
        {
            get
            {
                return new UsuarioService().Find(IdLiberadoPor);
            }
            set { }
        }

    }
}
