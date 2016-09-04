using System;
using System.Configuration;

namespace App.Web
{
    public class AppConfig
    {
        public static readonly string AppName = ConfigurationManager.AppSettings["brand:AppName"];
        public static readonly string AppVersion = ConfigurationManager.AppSettings["brand:AppVersion"];
        public static readonly string AppEnvironment = ConfigurationManager.AppSettings["brand:AppEnvironment"];
        public static readonly string CompanyName = ConfigurationManager.AppSettings["brand:CompanyName"];
        public static readonly string AppCookie = ConfigurationManager.AppSettings["Auth:AppCookie"];
        public static readonly int PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["View:PageSize"]);
    }
}