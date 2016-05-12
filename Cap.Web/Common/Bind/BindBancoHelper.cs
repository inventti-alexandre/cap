using Cap.Domain.Service.Cap;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindBancoHelper
    {
        public static MvcHtmlString SelectBanco(this HtmlHelper html, int idBanco = 0)
        {
            var bancos = new BancoService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdBanco");
            tag.MergeAttribute("name", "IdBanco");
            tag.MergeAttribute("class", "form-control");

            foreach (var item in bancos)
            {
                TagBuilder itemTag = new TagBuilder("select");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idBanco)
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