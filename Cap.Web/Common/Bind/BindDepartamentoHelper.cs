﻿using Cap.Domain.Service.Cap;
using System.Linq;
using System.Web.Mvc;

namespace Cap.Web.Common.Bind
{
    public static class BindDepartamentoHelper
    {
        public static MvcHtmlString SelectDepartamento(this HtmlHelper html, int idDepartamento = 0)
        {
            var departamentos = new DepartamentoService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Descricao)
                .ToList();

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttribute("id", "IdDepartamento");
            tag.MergeAttribute("name", "IdDepartamento");
            tag.MergeAttribute("class", "form-control");

            foreach (var item in departamentos)
            {
                TagBuilder itemTag = new TagBuilder("option");
                itemTag.MergeAttribute("value", item.Id.ToString());
                if (item.Id == idDepartamento)
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