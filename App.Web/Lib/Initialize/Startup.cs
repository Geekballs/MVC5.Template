using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace App.Web
{
    public static class MyAuthentication
    {
        public const string ApplicationCookie = "AppAuthenticationType";
    }

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder appBuilder)
        {
            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = MyAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = AppConfig.AppCookie,
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromHours(6),
            });
        }
    }
}
