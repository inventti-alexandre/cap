using Cap.Domain.Models.Cap;

namespace Cap.Domain.Abstract
{
    public interface ILogistica
    {
        void Concluir(Logistica logistica);
        void CancelarConclusao(int id);
    }
}
