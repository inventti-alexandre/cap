using Cap.Domain.Models.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cap.Web.Areas.Erp.Models
{
    public class IndicesVariacoes
    {
        public List<DateTime> DatasBases { get; set; }
        public IEnumerable<Indices> Indices { get; set; }
    }

    public class Indices
    {
        public Indice Indice { get; set; }
        public List<IndVariacao> Variacao { get; set; }
    }
}