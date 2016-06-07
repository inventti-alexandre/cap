using Cap.Domain.Models.Admin;
using System.Collections.Generic;

namespace Cap.Domain.Abstract.Admin
{
    public interface ITelaRegra
    {
        IEnumerable<TelaRegraModel> GetRegras(int idTela);
        void SetRegras(int idTela, int[] idRegras);
    }
}
