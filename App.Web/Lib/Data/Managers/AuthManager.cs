using System;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Managers
{
    public class AuthManager
    {
        public static User GetById(Guid id)
        {
            using (var ctx = new AppDbContext())
            {
                var movie = ctx.Users.Find(id);
                return movie;
            }
        }

        public static bool HasTrust(string name, string accessToken)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.UserRoles.Count(x => x.User.Name == name && x.User.Enabled && x.Role.Name == accessToken) > 0;
                return user;
            }
        }

        public static bool IsEnabled(string name)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.Count(x => x.Name == name && x.Enabled) > 0;
                return user;
            }
        }
    }
}