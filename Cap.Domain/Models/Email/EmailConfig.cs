using Cap.Domain.Models.Admin;
using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Cap.Domain.Models.Email
{
    public class EmailConfig
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Empresa")]
        [HiddenInput(DisplayValue = false)]
        [Range(0, double.MaxValue, ErrorMessage ="Empresa inválida")]
        public int IdEmpresa { get; set; }

        [Display(Name ="Email", Description ="Email utilizado para envio de cotações, alertas, etc...", Prompt ="seuemail@suaempresa.com.br")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Informe o email")]
        [StringLength(100, ErrorMessage ="O email é composto por no máximo 100 caracteres")]
        public string Sender { get; set; }

        [Display(Name ="Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Informe a senha")]
        [StringLength(20, ErrorMessage ="A senha é composta por no máximo 20 caracteres")]
        public string SenderPassword { get; set; }

        [Display(Name ="Porta", Description ="Porta utilizada pelo seu provedor de emails", Prompt ="587")]
        [Required(ErrorMessage ="Informe o número da porta")]
        public int ServerPort { get; set; }

        [Display(Name ="SMTP", Description ="Endereço do serviço de envio de mensagens do seu provedor de emails", Prompt ="smtp.suaempresa.com.br")]
        [Required(ErrorMessage ="Informe o endereço SMTP")]
        [StringLength(100, ErrorMessage ="O endereço SMTP é composto por no máximo 100 caracteres")]
        public string SenderSmtp { get; set; }

        [Display(Name ="Usar SSL")]
        public bool UseSSL { get; set; }

        [Display(Name ="Usar este email para enviar e-mails")]
        public bool Ativo { get; set; }

        [Display(Name ="Alterado por")]
        public int AlteradoPor { get; set; }

        [Display(Name ="Alterado em")]
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
