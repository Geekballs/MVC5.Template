using System;
using System.Web.Mvc;
using App.Web.Lib.Attributes;

namespace App.Web.Lib.Controllers
{
    public class AppController : BaseController
    {
        [Trust]
        public ActionResult Index()
        {
                  return View();
        }
    }
}