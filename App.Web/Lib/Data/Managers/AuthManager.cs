using System;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Managers
{
    public class AuthManager
    {
        public User GetUserById(Guid id)
        {
            using (var ctx = new AppDbContext())
            {
                var data = ctx.Users.Find(id);
                return data;
            }
        }

        public bool HasTrust(string name, string accessToken)
        {
            using (var ctx = new AppDbContext())
            {
                var data = ctx.UserRoles.Count(ur => ur.User.Name == name && ur.User.Enabled && ur.Role.Name == accessToken) > 0;
                return data;
            }
        }

        public bool IsEnabled(string name)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.Count(u => u.Name == name && u.Enabled) > 0;
                return user;
            }
        }
    }
}