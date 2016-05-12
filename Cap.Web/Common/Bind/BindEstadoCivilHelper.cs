using Cap.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindEstadoCivilHelper
    {
        public static MvcHtmlString SelectEstadoCivil(this HtmlHelper html, int idEstadoCivil = 0)
        {
            var estados = new EstadoCivilService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdEstadoCivil");
            tag.MergeAttribute("name", "IdEstadoCivil");
            tag.MergeAttribute("class", "form-control");

            foreach (var item in estados)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idEstadoCivil)
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