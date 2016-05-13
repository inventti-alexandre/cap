using Cap.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindEstadoHelper
    {
        public static MvcHtmlString SelectEstado(this HtmlHelper html, int idEstado = 0)
        {
            var estados = new EstadoService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.UF)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdEstado");
            tag.MergeAttribute("name", "IdEstado");
            tag.MergeAttribute("class", "form-control");

            foreach (var item in estados)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idEstado)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.SetInnerText(item.UF);
                tag.InnerHtml += itemTag.ToString();
            }
            return new MvcHtmlString(tag.ToString());
        }
    }
}