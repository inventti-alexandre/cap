using Cap.Domain.Service.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindIndiceHelper
    {
        public static MvcHtmlString SelectIndice(this HtmlHelper html, int idIndice = 0, bool selecione = false)
        {
            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdIndice");
            tag.MergeAttribute("name", "IdIndice");
            tag.MergeAttribute("class", "form-control");

            if (selecione == true)
            {
                TagBuilder itemSel = new TagBuilder("option");
                itemSel.MergeAttribute("value", "0");
                itemSel.SetInnerText("");
                tag.InnerHtml += itemSel.ToString();
            }

            var indices = new IndiceService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao)
                .ToList();

            foreach (var item in indices)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idIndice)
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