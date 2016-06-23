using Cap.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindUsuarioHelper
    {
        public static MvcHtmlString SelectUsuario(this HtmlHelper html, int idUsuario, bool selecione = false, string tagId = "IdUsuario", bool soAtivos = false)
        {
            var idEmpresa = new UsuarioService().GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var usuarios = new UsuarioService().Listar()
                .Where(x => x.IdEmpresa == idEmpresa &&
                (soAtivos == false || (x.Ativo == soAtivos)))
                .OrderBy(x => x.Nome)
                .ToList();

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

            foreach (var item in usuarios)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idUsuario)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.SetInnerText(item.Nome);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}