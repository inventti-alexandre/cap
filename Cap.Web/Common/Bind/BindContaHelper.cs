using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindContaHelper
    {
        public static MvcHtmlString SelectConta(this HtmlHelper html, int idConta)
        {
            var idEmpresa = new UsuarioService().GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var contas = new ContaService().Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdConta");
            tag.MergeAttribute("name", "IdConta");
            tag.MergeAttribute("class", "form-control");

            foreach (var item in contas)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idConta)
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