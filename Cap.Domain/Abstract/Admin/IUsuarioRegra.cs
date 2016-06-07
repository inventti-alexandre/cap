using Cap.Domain.Models.Admin;
using System.Collections.Generic;

namespace Cap.Domain.Abstract.Admin
{
    public interface IUsuarioRegra
    {
        IEnumerable<UsuarioRegraModel> GetRegras(int idUsuario);
        void SetRegras(int idUsuario, int[] idTelas, int[] idRegras);
    }
}
