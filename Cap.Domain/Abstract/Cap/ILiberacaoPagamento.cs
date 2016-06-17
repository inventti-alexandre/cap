using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Abstract.Cap
{
    public interface ILiberacaoPagamento
    {
        List<Parcela> ParcelasALiberar(DateTime? final);

        void LiberarParcelas(List<int> idParcelas, int idUsuario);

        List<Parcela> ParcelasACancelar(DateTime? final);

        void CancelarLiberacao(int idParcela, int idUsuario);
    }
}
