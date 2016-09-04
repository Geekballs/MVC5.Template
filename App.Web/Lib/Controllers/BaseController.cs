using System.Web.Mvc;
using App.Web.Lib.Data.Managers;

namespace App.Web.Lib.Controllers
{
    public class BaseController : Controller
    {
        #region Alert Helpers

        protected const string Danger = (@"danger");
        protected const string Info = (@"info");
        protected const string Success = (@"success");
        protected const string Warning = (@"warning");

        public ActionResult GetAlert(string alertType, string alertMessage)
        {
            TempData.Clear();
            TempData.Add(alertType, alertMessage);
            return PartialView("_Alert");
        }

        #endregion

        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "App");
        }

        protected readonly UserManager TheUserManager = new UserManager();
        protected readonly RoleManager TheRoleManager = new RoleManager();
    }
}