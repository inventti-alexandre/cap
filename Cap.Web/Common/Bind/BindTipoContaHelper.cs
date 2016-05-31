using Cap.Domain.Models.Cap;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindTipoContaHelper
    {
        public static MvcHtmlString TipoConta(this HtmlHelper html, TipoConta tipo)
        {
            var names = Enum.GetNames(typeof(TipoConta));
            var values = Enum.GetValues(typeof(TipoConta)).Cast<int>().ToArray();
            

            TagBuilder tag = new TagBuilder("ul");
            tag.MergeAttribute("class", "list-inline");

            for (int i = 0; i < names.Count(); i++)
            {
                var type = typeof(TipoConta);
                var enumAtual = (TipoConta)values[i];
                var member = type.GetMember(enumAtual.ToString());
                var attributes = member[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                var name = ((DisplayAttribute)attributes[0]).Name;

                TagBuilder itemTag = new TagBuilder("li");
                var radio = string.Format("<input type='radio' name='TipoConta' value='{0}' {2} />{1}", values[i], name, values[i] == (int)tipo ? "checked" : "");
                itemTag.InnerHtml += radio;
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }
    }
}