using Cap.Domain.Models.Cap;
using System;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindPeriodicidadeHelper
    {
        public static MvcHtmlString SelectPeriodicidade(this HtmlHelper html, Periodicidade periodicidade = Periodicidade.Mensal)
        {
            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "Periodicidade");
            tag.MergeAttribute("name", "Periodicidade");
            tag.MergeAttribute("class", "form-control");

            foreach (var item in Enum.GetValues(typeof(Periodicidade)))
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", ((int)item).ToString());
                if ((int)item == (int)periodicidade)
                {
                    itemTag.MergeAttribute("selected", "selected");
                }
                itemTag.InnerHtml = item.ToString();
                tag.InnerHtml += itemTag.ToString();
            }
            return new MvcHtmlString(tag.ToString());
        }
    }
}