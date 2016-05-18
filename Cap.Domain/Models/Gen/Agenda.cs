using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using Cap.Domain.Service.Gen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Cap.Domain.Models.Gen
{
    public class Agenda
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Empresa inválida")]
        [Display(Name = "Empresa")]
        [Range(1, double.MaxValue, ErrorMessage = "Empresa inválida")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage ="Informe o nome")]
        [StringLength(100, ErrorMessage ="O nome é composto por no máximo 100 caracteres")]
        public string Nome { get; set; }

        [StringLength(100, ErrorMessage ="O contato é composto por no máximo 100 caracteres")]
        public string Contato { get; set; }

        [Display(Name ="Endereço")]
        [StringLength(100, ErrorMessage = "O endereço é composto por no máximo 100 caracteres")]
        public string Endereco { get; set; }

        [StringLength(40, ErrorMessage = "O bairro é composto por no máximo 40 caracteres")]
        public string Bairro { get; set; }

        [StringLength(40, ErrorMessage = "A cidade é composta por no máximo 40 caracteres")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        [Range(0, double.MaxValue, ErrorMessage = "Selecione o Estado")]
        public int IdEstado { get; set; }

        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [StringLength(100, ErrorMessage ="O website é composto por no máximo 100 caracteres")]
        public string WebSite { get; set; }

        [Display(Name ="Observações")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

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
        [Display(Name = "Estado")]
        public virtual Estado Estado
        {
            get
            {
                return new EstadoService().Find(IdEstado);
            }
            set { }
        }

        [NotMapped]
        [Display(Name ="Emails")]
        public virtual IEnumerable<AgendaEmail> Emails
        {
            get
            {
                return new AgendaEmailService().Listar().Where(x => x.IdAgenda == Id).ToList();
            }
            set { }
        }

        [NotMapped]
        [Display(Name ="Telefones")]
        public virtual IEnumerable<AgendaTelefone> Telefones
        {
            get
            {
                return new AgendaTelefoneService().Listar().Where(x => x.IdAgenda == Id).ToList();
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
            set { }
        }
    }
}
