using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindSituacaoPagamentoHelper
    {
        public static MvcHtmlString SelectSituacaoPagamento(this HtmlHelper html, int idSituacao)
        {
            // 0 - em aberto
            // 1 - pago

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdSituacaoPagamento");
            tag.MergeAttribute("name", "IdSituacaoPagamento");
            tag.MergeAttribute("class", "form-control");

            TagBuilder emAberto = new TagBuilder("select");
            emAberto.MergeAttribute("value", "0");
            emAberto.SetInnerText("EM ABERTO");
            tag.InnerHtml += emAberto.ToString();

            TagBuilder pago = new TagBuilder("select");
            pago.MergeAttribute("value", "1");
            pago.SetInnerText("PAGO");
            tag.InnerHtml += pago.ToString();

            return new MvcHtmlString(tag.ToString());
        }
    }
}