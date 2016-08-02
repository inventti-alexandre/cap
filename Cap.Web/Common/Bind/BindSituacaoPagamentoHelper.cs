using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindSituacaoPagamentoHelper
    {
        public static MvcHtmlString SelectSituacaoPagamento(this HtmlHelper html, int idSituacao, bool selecione = false)
        {
            // 0 - em aberto
            // 1 - pago

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdSituacaoPagamento");
            tag.MergeAttribute("name", "IdSituacaoPagamento");
            tag.MergeAttribute("class", "form-control");

            if (selecione == true)
            {
                TagBuilder itemSel = new TagBuilder("option");
                itemSel.MergeAttribute("value", "0");
                itemSel.SetInnerText("");
                tag.InnerHtml += itemSel.ToString();
            }

            TagBuilder emAberto = new TagBuilder("option");
            emAberto.MergeAttribute("value", "1");
            emAberto.SetInnerText("EM ABERTO");
            tag.InnerHtml += emAberto.ToString();

            TagBuilder pago = new TagBuilder("option");
            pago.MergeAttribute("value", "2");
            pago.SetInnerText("PAGO");
            tag.InnerHtml += pago.ToString();

            return new MvcHtmlString(tag.ToString());
        }
    }
}