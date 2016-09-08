using System.Web.Mvc;
using App.Web.Lib.Data.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace App.Web
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IUserService, UserService>();
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}