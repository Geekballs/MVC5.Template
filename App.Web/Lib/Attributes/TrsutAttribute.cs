using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Web.Lib.Attributes
{
    public class TrustAttribute : AuthorizeAttribute
    {
        public string Privilege { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext ctx)
        {
            if (!ctx.HttpContext.User.Identity.IsAuthenticated)
            {
                ctx.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Auth",
                    action = "SignIn"
                }));
            }
            else
            {
                ctx.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "Http403"
                }));
            }
        }

        protected override bool AuthorizeCore(HttpContextBase ctx)
        {
            var isAuthorized = ctx.User.Identity.IsAuthenticated;
            var identity = (ClaimsPrincipal) Thread.CurrentPrincipal;
            var doesUserHaveRequiredRole = identity.Claims.Count(c => c.Type == ClaimTypes.Role && c.Value == Privilege) > 0;
            if (isAuthorized && Privilege != null)
            {
                return doesUserHaveRequiredRole;
            }
            return isAuthorized;
        }
    }
}