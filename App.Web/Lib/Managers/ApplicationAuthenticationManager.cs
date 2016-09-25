using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Managers
{
    /// <summary>
    /// Really should be using the IUserService instead of these methods!
    /// </summary>
    public class ApplicationAuthenticationManager
    {
        public static bool IsUserEnabled (string name)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.Count(p => p.UserName == name && p.LoginEnabled) > 0;
                return user;
            }
        }

        public static User GetUserByName(string name)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.First(p => p.UserName == name);
                return user;
            }
        }

        public static IEnumerable<UserRole> GetRolesForUser(Guid userId)
        {
            using (var ctx = new AppDbContext())
            {
                var userRoles = ctx.UserRoles.Include(t => t.Role).Where(p => p.UserId == userId).ToList();
                return userRoles;
            }
        }
    }
}