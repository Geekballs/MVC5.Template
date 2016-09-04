using System.Web;
using System.Web.Mvc;
using App.Web.Lib.Services;
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

            // usually this will be injected via DI. but creating this manually now for brevity
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);

            var authenticationResult = authService.SignIn(model.Username, model.Password);

            if (authenticationResult.IsSuccess)
            {
                // we are in!
                return RedirectToAction("Index", "App");
                //return RedirectToLocal(returnUrl);
            }
            GetAlert(Danger, authenticationResult.ErrorMessage);
            ModelState.AddModelError("", authenticationResult.ErrorMessage);
            return View(model);
        }

        [Route("Sign-Out"), HttpPost]
        //[ValidateAntiForgeryToken]
        public virtual ActionResult SignOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);

            return RedirectToAction("SignIn", "Auth");
        }


        
    }
}
