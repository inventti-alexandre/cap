using Cap.Domain.Service.Admin;
using System.Linq;
using System.Web;

namespace Cap.Web.Common
{
    public static class Identification
    {
        public static int IdUsuario
        {
            get
            {
                return new UsuarioService().Listar()
                    .FirstOrDefault(x => x.Email.ToUpper() == HttpContext.Current.User.Identity.Name.ToUpper()).Id;
            }
        }

        public static string NomeUsuario
        {
            get
            {
                return new UsuarioService().Listar()
                    .FirstOrDefault(x => x.Email.ToUpper() == HttpContext.Current.User.Identity.Name.ToUpper()).Nome;
            }
        }
    }
}