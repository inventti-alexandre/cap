﻿using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Requisicao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Cap.Domain.Models.Requisicao
{
    public class CotCotadoCom
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Requisicao")]
        [Range(1,double.MaxValue, ErrorMessage ="Requisição inválida")]
        public int ReqRequisicaoId { get; set; }

        [Display(Name ="Fornecedor")]
        [Range(1,double.MaxValue,ErrorMessage ="Fornecedor inválido")]
        public int FornecedorId { get; set; }

        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email do fornecedor inválido")]
        [StringLength(100, ErrorMessage ="O email do fornecedor é composto por no máximo 100 caracteres")]
        public string Email { get; set; }

        public bool Preenchida { get; set; }

        [Display(Name ="Alterado em")]
        [Required]
        public DateTime AlteradoEm { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage ="Usuário inválido")]
        public int UsuarioId { get; set; }

        [Display(Name = "Guid")]
        [Required(ErrorMessage ="Guid inválido")]
        public string Guid { get; set; }

        [Display(Name = "Requisição")]
        public virtual ReqRequisicao Requisicao { get; set; }

        [Display(Name = "Usuário")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name ="Fornecedor")]
        public virtual Fornecedor Fornecedor { get; set; }

        [NotMapped]
        public virtual CotDadosCotacao DadosCotacao
        {
            get
            {
                return new CotDadosCotacaoService().Listar()
                    .Where(x => x.CotCotadoComId == Id)
                    .FirstOrDefault();
            }
        }

        [NotMapped]
        public virtual List<CotCotacao> Cotacao
        {
            get
            {
                return new CotCotacaoService().Listar()
                    .Where(x => x.ReqRequisicaoId == ReqRequisicaoId
                        && x.FornecedorId == FornecedorId)
                        .ToList();                    
            }
        }
    }
}
