using Cap.Domain.Models.Cap;

namespace Cap.Domain.Abstract.Cap
{
    public interface IInfoCaixa
    {
        InfoCaixa GetInfoCaixa(int idEmpresa, int idUsuario);
    }
}
