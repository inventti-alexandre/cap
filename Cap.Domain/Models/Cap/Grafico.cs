using System;

namespace Cap.Domain.Models.Cap
{
    public class Grafico
    {
        public DateTime Data { get; set; }
        public string Dia { get; set; }
        public decimal Valor { get; set; }
        public DateTime Inicial { get; set; }
        public DateTime Final { get; set; }
        public bool Feriado { get; set; }
    }
}
