using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using App.Web.Lib.Data.Managers;

namespace App.Web.Lib.Attributes
{
    public class TrustAttribute : AuthorizeAttribute
    {
        AuthManager TheAuthManager = new AuthManager();

        public string AccessToken { get; set; }

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
                    controller = "Auth",
                    action = "SignOut"
                }));
            }
        }

        protected override bool AuthorizeCore(HttpContextBase ctx)
        {
            var user = ctx.User.Identity.Name;
            var isAuthorized = ctx.User.Identity.IsAuthenticated;

            if (isAuthorized && AccessToken != null)
            {
                return TheAuthManager.HasTrust(user, AccessToken);
            }
            if (isAuthorized)
            {
                return TheAuthManager.IsEnabled(user);
            }
            return false;
        }
    }

    
}