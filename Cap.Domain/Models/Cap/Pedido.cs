using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cap.Domain.Models.Admin;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;

namespace Cap.Domain.Models.Cap
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Empresa inválida")]
        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Empresa inválida")]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int IdEmpresa { get; set; }

        [Range(1,Double.MaxValue,ErrorMessage ="Selecione o departamento")]
        [Display(Name ="Departamento")]
        public int IdDepartamento { get; set; }

        [Range(1, double.MaxValue,ErrorMessage ="Selecione o fornecedor")]
        [Display(Name ="Fornecedor")]
        public int IdFornecedor { get; set; }

        [Required(ErrorMessage ="Data da criação do pedido inválida")]
        [Display(Name ="Criado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CriadoEm { get; set; }

        [Range(1,double.MaxValue, ErrorMessage ="Não há usuário responsável pela criação do pedido")]
        [Display(Name = "Criado por")]
        public int CriadoPor { get; set; }

        [StringLength(20,ErrorMessage ="A nota fiscal é composta por no máximo 20 caracteres")]
        [Display(Name = "Nota fiscal")]
        public string NF { get; set; }

        [Display(Name ="Data da nota fiscal")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataNF { get; set; }

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
        }

        [NotMapped]
        [Display(Name ="Criado por")]
        public virtual Usuario CriadoPorUsuario
        {
            get
            {
                return new UsuarioService().Find(CriadoPor);
            }
        }

        [NotMapped]
        [Display(Name = "Departamento")]
        public virtual Departamento Departamento
        {
            get
            {
                return new DepartamentoService().Find(IdDepartamento);
            }
            set { }
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
        [Display(Name = "Parcelas")]
        public List<Parcela> Parcelas
        {
            get
            {
                return new ParcelaService().Listar().Where(x => x.IdPedido == Id).ToList();
            }
        }
    }
}
