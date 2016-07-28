using Cap.Domain.Models.Cap;
using Cap.Domain.Service.Cap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap.Domain.Abstract.Cap
{
    public interface ICaixa
    {
        List<Parcela> GetParcelas(int idEmpresa, DateTime inicial, DateTime final, int idDepartamento, int idFornecedor, int idPgto);
        List<Parcela> BaixarParcelas(List<int> idParcelas, int idUsuario, int idConta, int idCheque, DateTime caixaDia);
        List<Parcela> EstornarCheque(int idConta, int idCheque, int idUsuario);
        DataCaixa DataCaixa();
    }
}
