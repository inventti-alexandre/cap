using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;

namespace Cap.Domain.Abstract.Cap
{
    public interface IGrafico
    {
        List<Grafico> GetGrafico(DateTime inicial, DateTime final, int idEmpresa, int idDepartamento = 0, int idPgto = 0);
    }
}
