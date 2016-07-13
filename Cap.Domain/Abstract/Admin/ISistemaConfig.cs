using Cap.Domain.Models.Admin;

namespace Cap.Domain.Abstract.Admin
{
    public interface ISistemaConfig
    {
        SistemaConfig GetConfig(int idEmpresa);
        void SetConfig(SistemaConfig config, int idUsuario);
    }
}
