using System.Web;
using System.Web.Mvc;
using App.Web.Lib.Managers;
using App.Web.Lib.ViewModels;

namespace App.Web.Lib.Controllers
{
    [AllowAnonymous, RoutePrefix("Auth")]
    public class AuthController : BaseController
    {
        [Route("Sign-In"), HttpGet]
        public virtual ActionResult SignIn()
        {
            return View();
        }

        [Route("Sign-In"), HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult SignIn(AuthVm.SignIn model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var authMgr = HttpContext.GetOwinContext().Authentication;
            var authSvc = new ActiveDirectoryAuthenticationManager(authMgr);
            var authRes = authSvc.SignIn(model.Username, model.Password);
            if (authRes.IsSuccess)
            {
                return RedirectToAction("Index", "App");
            }
            GetAlert(Danger, authRes.ErrorMessage);
            ModelState.AddModelError("", authRes.ErrorMessage);
            return View(model);
        }

        [Route("Sign-Out"), HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult SignOut()
        {
            var authMgr = HttpContext.GetOwinContext().Authentication;
            authMgr.SignOut(MyAuthentication.ApplicationCookie);
            return RedirectToAction("SignIn", "Auth");
        }
    }
}