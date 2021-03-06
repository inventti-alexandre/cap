﻿using Cap.Domain.Models.Cap;
using System;
using System.Collections.Generic;

namespace Cap.Domain.Abstract.Cap
{
    public interface ILiberacao
    {
        List<Parcela> ParcelasALiberar(int idUsuario, DateTime? final);

        void LiberarParcelas(List<int> idParcelas, int idUsuario);

        List<Parcela> ParcelasACancelar(int idUsuario, DateTime? final);

        void CancelarLiberacao(int idParcela, int idUsuario);
    }
}
