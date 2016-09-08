using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using App.Web.Lib.ViewModels;

namespace App.Web.Lib.Controllers
{
    [RoutePrefix("Layout")]
    public class LayoutController : BaseController
    {
        [Route("Header")]
        [ChildActionOnly]
        public ActionResult Header()
        {
            var model = new AuthVm.UserProperties();

            if (User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity) User.Identity;
                var userName = identity.FindFirst(ClaimTypes.Name);
                var userRoles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                var roles = userRoles as string[] ?? userRoles.ToArray();
                if (roles.Contains("Super Admin"))
                {
                    model.IsSuperAdminRole = true;
                }
                if (roles.Contains("Admin"))
                {
                    model.IsAdminRole = true;
                }
                model.IsAuthenticated = true;
                model.Username = userName.Value;
            }

            return PartialView("_Header", model);
        }

        [Route("Footer")]
        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView("_Footer");
        }
    }
}