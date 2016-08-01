using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cap.Web.Areas.Erp.Models
{
    public class BaixarParcelasSelecionadasModel
    {
        [Display(Name ="Conta")]
        public int IdConta { get; set; }
        public int Cheque { get; set; }
        [Display(Name ="Parcelas selecionadas")]
        public List<int> Selecionados { get; set; }
        [Display(Name = "Data compensação")]
        public string DataCompensacao { get; set; }
    }
}