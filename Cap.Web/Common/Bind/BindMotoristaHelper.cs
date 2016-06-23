using Cap.Domain.Respository;
using Cap.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindMotoristaHelper
    {
        public static MvcHtmlString SelectMotorista(this HtmlHelper html, int idMotorista, bool selecione = false, string tagId = "MotoristaId", bool soAtivos = false)
        {
            var idEmpresa = new UsuarioService().GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var db = new EFDbContext();

            var motoristas = (from u in db.Usuario
                              join m in db.Motorista on u.Id equals m.UsuarioId
                              where u.IdEmpresa == idEmpresa
                              select m).ToList()
                              .Where(x => (soAtivos == false || (x.Ativo == soAtivos)))
                              .ToList()
                              .OrderBy(x => x.Usuario.Nome);

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", tagId);
            tag.MergeAttribute("name", tagId);
            tag.MergeAttribute("class", "form-control");

            if (selecione == true)
            {
                TagBuilder itemSel = new TagBuilder("option");
                itemSel.MergeAttribute("value", "0");
                itemSel.SetInnerText("");
                tag.InnerHtml += itemSel.ToString();
            }

            foreach (var item in motoristas)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idMotorista)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.SetInnerText(item.Usuario.Nome);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}