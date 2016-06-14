using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Abstract.Gen
{
    public interface IIndVariacaoCalculo
    {
        decimal CalcularVariacao(int idIndice, DateTime inicial, DateTime final);
    }
}
