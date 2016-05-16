using Cap.Domain.Models.Admin;

namespace Cap.Domain.Abstract.Admin
{
    public interface ILogin
    {
        Usuario ValidaLogin(string email, string senha);
        int GetIdUsuario(string email);
        Usuario GetUsuario(string email);
    }
}
