using Cap.Domain.Service.Admin;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindSistemaAreaHelper
    {
        public static MvcHtmlString SelectSistemaArea(this HtmlHelper html, int idSistemaArea = 0, bool selecione = false)
        {
            var areas = new SistemaAreaService().Listar()
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdSistemaArea");
            tag.MergeAttribute("name", "IdSistemaArea");
            tag.MergeAttribute("class", "form-control");

            if (selecione == true)
            {
                TagBuilder tagSel = new TagBuilder("option");
                tagSel.MergeAttribute("value", "0");
                tagSel.SetInnerText("");
                tag.InnerHtml += tagSel.ToString();
            }

            foreach (var item in areas)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idSistemaArea)
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