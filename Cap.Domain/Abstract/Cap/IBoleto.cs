using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;

namespace Cap.Domain.Abstract.Cap
{
    public interface IBoleto
    {
        List<Parcela> GetBoletos(int idEmpresa, DateTime? inicial, DateTime? final);
        Parcela SetBoleto(int idParcela, string nn, int idUsuario, int idEmpresa);
    }
}
