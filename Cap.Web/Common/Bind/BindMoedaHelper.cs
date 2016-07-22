using Cap.Domain.Service.Admin;
using Cap.Domain.Service.Cap;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindMoedaHelper
    {
        public static MvcHtmlString SelectMoeda(this HtmlHelper html, int idMoeda = 0, bool selecione = false, string idTag = "IdMoeda")
        {
            var idEmpresa = new UsuarioService().GetUsuario(System.Web.HttpContext.Current.User.Identity.Name).IdEmpresa;

            var moedas = new MoedaService().Listar()
                .Where(x => x.IdEmpresa == idEmpresa)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", idTag);
            tag.MergeAttribute("name", idTag);
            tag.MergeAttribute("class", "form-control");
            tag.MergeAttribute("style", "width:80px;");

            if (selecione == true)
            {
                TagBuilder itemSel = new TagBuilder("option");
                itemSel.MergeAttribute("value", "0");
                itemSel.SetInnerText("");
                tag.InnerHtml += itemSel.ToString();
            }

            foreach (var item in moedas)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (idMoeda != 0)
                {
                    if (item.Id == idMoeda)
                    {
                        itemTag.MergeAttribute("selected", "selected");
                    }
                }
                else
                {
                    if (item.Padrao == true)
                    {
                        itemTag.MergeAttribute("selected", "selected");
                    }
                }
                itemTag.SetInnerText(item.Descricao);
                tag.InnerHtml += itemTag.ToString();
            }
            return new MvcHtmlString(tag.ToString());
        }
    }
}