using Cap.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindSistemaRegraHelper
    {
        public static MvcHtmlString SelectSistemaRegra(this HtmlHelper html, int idSistemaRegra = 0, bool selecione = false)
        {
            var regras = new SistemaRegraService().Listar()
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdSistemaRegra");
            tag.MergeAttribute("name", "IdSistemaRegra");
            tag.MergeAttribute("class", "form-control");

            if (selecione == true)
            {
                TagBuilder itemSel = new TagBuilder("option");
                itemSel.MergeAttribute("value", "0");
                itemSel.SetInnerText("");
                tag.InnerHtml += itemSel.ToString();
            }

            foreach (var item in regras)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idSistemaRegra)
                {
                    itemTag.MergeAttribute("select", "select");
                }
                itemTag.SetInnerText(item.Descricao);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}