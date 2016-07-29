using System.Collections.Generic;

namespace Cap.Web.Areas.Erp.Models
{
    public class BaixarParcelasSelecionadasModel
    {
        public int IdConta { get; set; }
        public int Cheque { get; set; }
        public List<int> Selecionados { get; set; }
        public string DataCompensacao { get; set; }
    }
}