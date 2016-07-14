using System.Web.Mvc;

namespace Cap.Web.Areas.Erp
{
    public class ErpAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Erp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Requisicao_autorizante",
                "Erp/Requisicao-autorizante/{action}/{id}",
                new { action = "Index", Controller = "ReqAutorizante", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Confirmar_entrega",
                "Erp/Confirmar-entrega/{id}",
                new { action = "ConfirmarEntrega", Controller = "Requisicao", id = 0 },
                new { id = @"\d+"}
            );

            context.MapRoute(
                "Trocar_senha",
                "Erp/Trocar-senha/{action}/{id}",
                new { action = "TrocarSenha", Controller = "Usuario", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Erp_default",
                "Erp/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}