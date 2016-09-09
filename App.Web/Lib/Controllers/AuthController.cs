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
            var authManager = HttpContext.GetOwinContext().Authentication;
            var authService = new ActiveDirectoryAuthenticationManager(authManager);
            var authResult = authService.SignIn(model.Username, model.Password);
            if (authResult.IsSuccess)
            {
                return RedirectToAction("Index", "App");
            }
            GetAlert(Danger, authResult.ErrorMessage);
            ModelState.AddModelError("", authResult.ErrorMessage);
            return View(model);
        }

        [Route("Sign-Out"), HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult SignOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut(MyAuthentication.ApplicationCookie);
            return RedirectToAction("SignIn", "Auth");
        }
    }
}