using Cap.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Models.Admin
{
    public class UsuarioRegraModel
    {
        public int IdUsuario { get; set; }
        public int IdTela { get; set; }
        public int IdRegra { get; set; }
        public string Sufixo { get; set; }
        public bool Selecionado { get; set; }

        [Display(Name = "Usuário")]
        public virtual Usuario Usuario
        {
            get
            {
                return new UsuarioService().Find(IdUsuario);
            }
        }

        [Display(Name ="Tela")]
        public virtual SistemaTela SistemaTela
        {
            get
            {
                return new SistemaTelaService().Find(IdTela);
            }
        }

        [Display(Name = "Regra")]
        public virtual SistemaRegra SistemaRegra
        {
            get
            {
                return new SistemaRegraService().Find(IdRegra);
            }
        }
    }
}
