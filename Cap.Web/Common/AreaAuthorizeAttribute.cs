using System.Web.Mvc;

namespace Cap.Web.Common
{
    public class AreaAuthorizeAttribute: AuthorizeAttribute
    {
        private readonly string area;

        public AreaAuthorizeAttribute(string area)
        {
            this.area = area;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string loginUrl = "";

            if (area == "Erp")
            {
                loginUrl = "~/Erp/Login";
            }

            filterContext.Result = new RedirectResult(loginUrl + "?returnUrl=" + filterContext.HttpContext.Request.Url.PathAndQuery);
        }
    }
}