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
                model.IsAuthenticated = true;
                model.Username = User.Identity.Name;
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