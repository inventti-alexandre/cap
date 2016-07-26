using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindDepartamentoHelper
    {
        public static MvcHtmlString SelectDepartamento(this HtmlHelper html, int idDepartamento = 0, bool selecione = false, string idTag = "IdDepartamento")
        {
            var idEmpresa = new UsuarioService().GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var departamentos = new DepartamentoService().Listar()
                .Where(x => x.Ativo == true && x.IdEmpresa == idEmpresa)
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

            foreach (var item in departamentos)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idDepartamento)
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