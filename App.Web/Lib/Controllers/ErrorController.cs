using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Lib.Controllers
{
    public class ErrorController : BaseController
    {
        #region Standard Error Pages

        public ActionResult Generic()
        {
            Response.StatusCode = 500;
            return View();
        }

        [Route("400")]
        public ActionResult Http400()
        {
            Response.StatusCode = 404;
            return View();
        }

        [Route("403")]
        public ActionResult Http403()
        {
            // This is a bit dirty, but it forces a user to sign out if they have been previously authenticated.
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut(MyAuthentication.ApplicationCookie);
            HttpContext.User = new GenericPrincipal(new GenericIdentity(String.Empty), null); 
            Response.StatusCode = 403;
            return View();
        }

        [Route("404")]
        public ActionResult Http404()
        {
           Response.StatusCode = 404;
           return View();
        }

        [Route("500")]
        public ActionResult Http500()
        {
            Response.StatusCode = 500;
            return View();
        }

        #endregion  
    }
}