using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using Cap.Domain.Service.Requisicao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Cap.Domain.Models.Requisicao
{
    public enum Situacao
    {
        [Description("Em digitação")]
        EmDigitacao = 0,
        [Description("Cotar")]
        Cotar = 1,
        [Description("Em cotação")]
        EmCotacao = 2,
        [Description("Cotado")]
        Cotado = 3,
        [Description("Cancelada")]
        Cancelada = 4,
        [Description("Aprovada")]
        Aprovada = 5,
        [Description("Comprada")]
        Comprada = 6,
        [Description("Entregue")]
        Entregue = 7
    }

    public class ReqRequisicao
    {
        [Key]
        public int Id { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Selecione o departamento")]
        [Display(Name = "Departamento")]
        public int IdDepartamento { get; set; }

        [Display(Name = "Grupo de custo")]
        [Required(ErrorMessage = "Selecione o grupo de custo")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione o grupo de custo")]
        public int GrupoCustoId { get; set; }

        [Display(Name = "Centro de custo")]
        [Required(ErrorMessage = "Selecione o centro de custo")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione o centro de custo")]
        public int CentroCustoId { get; set; }

        [Required(ErrorMessage = "Informe a data de solicitação da requisição")]
        [Display(Name = "Solicitado em")]
        public DateTime SolicitadoEm { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Requisitante inválido")]
        [Display(Name = "Solicitado por")]
        public int IdSolicitadoPor { get; set; }

        [Required(ErrorMessage = "Informe a data máxima para cotação")]
        [Display(Name = "Cotar até")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime CotarAte { get; set; }

        [Required(ErrorMessage = "Informe a data de entrega")]
        [Display(Name = "Entregar dia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime EntregarDia { get; set; }

        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

        [Display(Name = "Entrega noturna")]
        public bool EntregaNoturna { get; set; }

        public Situacao Situacao { get; set; }

        [Display(Name = "Cotado por")]
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

        [Display(Name = "Observações da liberação")]
        [DataType(DataType.MultilineText)]
        public string LiberadoObserv { get; set; }

        [Display(Name = "PedidoId")]
        public int? PedidoId { get; set; }

        [Display(Name = "Logística")]
        public int? LogisticaId { get; set; }

        [Display(Name = "Data compra")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime? CompradoEm { get; set; }

        [Display(Name = "Entregue em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime? EntregueEm { get; set; }

        [Display(Name = "Entrega confirmada por")]
        public int? EntregaConfirmadaPor { get; set; }

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

        [NotMapped]
        [Display(Name = "Materiais")]
        public virtual IEnumerable<ReqMaterial> ReqMaterial
        {
            get
            {
                return new ReqMaterialService().Listar()
                    .Where(x => x.IdReqRequisicao == Id)
                    .OrderBy(x => x.Id)
                    .ToList();
            }
        }

        [NotMapped]
        [Display(Name = "Cotado com")]
        public virtual IEnumerable<CotCotadoCom> CotadoCom
        {
            get
            {
                return new CotCotadoComService().Listar()
                    .Where(x => x.ReqRequisicaoId == Id)
                    .ToList();
            }
        }

        [NotMapped]
        [Display(Name = "Logística")]
        public virtual Logistica Logistica { get; set; }

        [NotMapped]
        [Display(Name = "Pedido")]
        public virtual Pedido Pedido { get; set; }

        [NotMapped]
        [Display(Name = "Grupo de custo")]
        public virtual GrupoCusto GrupoCusto
        {
            get
            {
                return new GrupoCustoService().Find(GrupoCustoId);
            }
        }

        [NotMapped]
        [Display(Name = "Centro de custo")]
        public virtual CentroCusto CentroCusto
        {
            get
            {
                return new CentroCustoService().Find(CentroCustoId);
            }
        }
    }
}
