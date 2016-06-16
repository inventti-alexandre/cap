using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Abstract.Cap
{
    public interface ILiberacao
    {
        List<Parcela> ParcelasALiberar(int idUsuario, DateTime? final);
        void LiberarParcelas(List<int> idParcelas, int idUsuario);

        void CancelarLiberacao(int idParcela, int idUsuario);
    }
}
