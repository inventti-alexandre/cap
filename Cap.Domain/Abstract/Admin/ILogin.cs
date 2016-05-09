using Cap.Domain.Models.Admin;

namespace Cap.Domain.Abstract.Admin
{
    public interface ILogin
    {
        Usuario ValidaLogin(string login, string senha);
        int GetIdUsuario(string login);
    }
}
