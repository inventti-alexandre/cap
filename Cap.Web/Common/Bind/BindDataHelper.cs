using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindDataHelper
    {
        public static MvcHtmlString SelectMes(this HtmlHelper html, int mes, bool selecione = false, string idTag = "")
        {
            if (string.IsNullOrEmpty(idTag))
            {
                idTag = "mes";
            }

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

            var lista = new List<string>();
            lista.Add("JANEIRO");
            lista.Add("FEVEREIRO");
            lista.Add("MARÇO");
            lista.Add("ABRIL");
            lista.Add("MAIO");
            lista.Add("JUNHO");
            lista.Add("JULHO");
            lista.Add("AGOSTO");
            lista.Add("SETEMBRO");
            lista.Add("OUTUBRO");
            lista.Add("NOVEMBRO");
            lista.Add("DEZEMBRO");

            for (int i = 0; i < lista.Count; i++)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", (i + 1).ToString());
                if ((i + 1) == mes)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.SetInnerText(lista[i].ToString());
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}