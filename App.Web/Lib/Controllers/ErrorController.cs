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