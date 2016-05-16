using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Requisicao;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindMatGrupoHelper
    {
        public static MvcHtmlString SelectMatGrupo(this HtmlHelper html, int idMatGrupo = 0)
        {
            var idEmpresa = new UsuarioService().GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var grupos = new MatGrupoService().Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdMatGrupo");
            tag.MergeAttribute("name", "IdMatGrupo");
            tag.MergeAttribute("class", "form-control");

            foreach (var item in grupos)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idMatGrupo)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.SetInnerText(item.Descricao);
                tag.InnerHtml += itemTag.ToString();
            }
            return new MvcHtmlString(tag.ToString());
        }
    }
}