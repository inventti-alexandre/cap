﻿using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Cap.Domain.Models.Cap
{
    public class Parcela
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Empresa inválida")]
        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Empresa inválida")]
        [HiddenInput(DisplayValue = false)]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage ="Pedido inválido")]
        [Display(Name ="Pedido")]
        [Range(1,double.MaxValue, ErrorMessage ="Pedido inválido")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage ="Informe a data de vencimento da parcela")]
        [Display(Name ="Vencimento")]
        [DataType(DataType.Date)]
        public DateTime Vencto { get; set; }

        [Required(ErrorMessage ="Informe o valor da parcela")]
        [Range(0.01,double.MaxValue, ErrorMessage ="Valor da parcela inválido")]
        public decimal Valor { get; set; }

        [Display(Name ="Pagável em")]
        [Range(0,double.MaxValue,ErrorMessage ="Informe a forma prevista de pagamento")]
        public int IdPgto { get; set; }

        [Display(Name ="Depósito")]
        [HiddenInput(DisplayValue = false)]
        public int? IdDeposito { get; set; }

        [Display(Name ="Emissão cheque pré-datado")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy")]
        [DataType(DataType.Date)]
        public DateTime? EmissaoPre { get; set; }

        [Display(Name = "Nosso número")]
        public string NN { get; set; }

        [Display(Name = "Data nosso número")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? NNData { get; set; }

        [Display(Name = "Nosso número atribuído por")]
        public int? NNPor { get; set; }

        [Display(Name ="Forma de pagamento")]
        public int? IdFpgto { get; set; }

        [Display(Name ="Observações")]
        public string Observ { get; set; }

        public bool Liberado { get; set; }

        [Display(Name = "Liberado por")]
        public int? LiberadoPor { get; set; }

        [Display(Name ="Liberado em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? LiberadoEm { get; set; }

        [Display(Name = "Moeda")]
        [Range(1,double.MaxValue,ErrorMessage ="Selecioe a moeda")]
        public int IdMoeda { get; set; }

        [Required]
        [Display(Name ="Data cadastro")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime CriadoEm { get; set; }

        [Required]
        [Display(Name ="Criado por")]
        [Range(1,double.MaxValue,ErrorMessage ="Usuário responsável pela criação da parcela inválido")]
        public int CriadoPor { get; set; }

        [Display(Name ="Liberado master")]
        public bool LibMaster { get; set; }

        [Display(Name ="Liberado master por")]
        public int? LibMasterPor { get; set; }

        [Display(Name ="Liberado master em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? LibMasterEm { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        [DataType(DataType.DateTime)]
        public DateTime AlteradoEm { get; set; }

        [Display(Name ="Conta")]
        public int? ContaId { get; set; }

        [Display(Name ="Cheque")]
        public int? Cheque { get; set; }

        [Display(Name ="Baixado em")]
        public DateTime? BaixadoEm { get; set; }

        [Display(Name = "Baixado por")]
        public int? BaixadoPor { get; set; }

        [Display(Name ="Caixa do dia")]
        public DateTime? DataCaixa { get; set; }

        [Display(Name ="Compensado")]
        public bool CompBanco { get; set; }

        [Display(Name ="Compensado em")]
        public DateTime? CompBancoData { get; set; }

        [Display(Name ="Conta")]
        public virtual Conta Conta { get; set; }

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
        [Display(Name = "Empresa")]
        public virtual Empresa Empresa
        {
            get
            {
                return new EmpresaService().Find(IdEmpresa);
            }
        }

        [NotMapped]
        [Display(Name = "Pedido")]
        public virtual Pedido Pedido
        {
            get
            {
                return new PedidoService().Find(IdPedido);
            }
        }

        [NotMapped]
        [Display(Name = "Liberado por")]
        public virtual Usuario LiberadoPorUsuario
        {
            get
            {
                if (LiberadoPor == null)
                {
                    return new Usuario() { Nome = string.Empty };
                }
                return new UsuarioService().Find((int)LiberadoPor);
            }
        }

        [NotMapped]
        [Display(Name = "Criado por")]
        public virtual Usuario CriadoPorUsuario
        {
            get
            {
                return new UsuarioService().Find((int)CriadoPor);
            }
        }

        [NotMapped]
        [Display(Name = "Liberado master por")]
        public virtual Usuario LibMasterPorUsuario
        {
            get
            {
                if (LibMasterPor == null)
                {
                    return new Usuario() { Nome = string.Empty };
                }
                return new UsuarioService().Find((int)LibMasterPor);
            }
        }

        [NotMapped]
        [Display(Name = "Pagável em")]
        public virtual Pgto Pgto
        {
            get
            {
                return new PgtoService().Find(IdPgto);
            }
        }

        [NotMapped]
        [Display(Name = "Moeda")]
        public virtual Moeda Moeda
        {
            get
            {
                return new MoedaService().Find(IdMoeda);
            }
        }

        [NotMapped]
        [Display(Name = "FPgto")]
        public virtual FPgto Fpgto
        {
            get
            {
                if (IdFpgto == null || IdFpgto == 0)
                {
                    return new FPgto();
                }

                var fpgto = new FPgtoService().Find((int)IdFpgto);

                if (fpgto == null)
                {
                    return new FPgto();
                }

                return fpgto;
            }
        }

        [NotMapped]
        [Display(Name = "Pago")]
        public virtual bool Pago
        {
            get
            {
                return (IdFpgto != null);
            }
        }
    }
}
