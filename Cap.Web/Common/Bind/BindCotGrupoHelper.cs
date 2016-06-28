using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Requisicao;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindCotGrupoHelper
    {
        public static MvcHtmlString SelectCotGrupo(this HtmlHelper html, int idCotGrupo = 0, bool selecione = false, string idTag = "IdCotGrupo" )
        {
            var idEmpresa = new UsuarioService().GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var grupos = new CotGrupoService().Listar()
                .Where(x => x.EmpresaId == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", idTag);
            tag.MergeAttribute("name", idTag);
            tag.MergeAttribute("class", "form-control");

            if (selecione == true)
            {
                TagBuilder itemSel = new TagBuilder("option");
                itemSel.MergeAttribute("value", "0");
                itemSel.SetInnerText("");
                tag.InnerHtml += itemSel.ToString();
            }

            foreach (var item in grupos)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idCotGrupo)
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