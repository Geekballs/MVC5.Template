using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Managers
{
    public class UserManager
    {
        #region Get

        public IEnumerable<User> GetAllRoles()
        {
            using (var ctx = new AppDbContext())
            {
                var users = ctx.Users.Include(ur => ur.UserRoles).OrderBy(u => u.Name).ToList();
                return users;
            }
        }

        public User GetUserById(Guid? id)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.Include(ur => ur.UserRoles).First(u => u.UserId == id);
                return user;
            }
        }

        public List<UserRole> GetRolesForUser(Guid? id)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.UserRoles.Include(r => r.Role).Where(ur => ur.UserId == id).ToList();
                return user;
            }
        }

        #endregion

        #region Create

        public void CreateUser(string name, bool enabled, bool locked, IEnumerable<Guid> roles)
        {
            using (var ctx = new AppDbContext())
            {
                var user = new User()
                {
                    Name = name,
                    Enabled = enabled,
                    Locked = locked
                };
                foreach (var roleId in roles)
                {
                    var role = ctx.Roles.Find(roleId);
                    user.UserRoles.Add(new UserRole()
                    {
                        RoleId = role.RoleId,
                        UserId = user.UserId
                    });
                }
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }

        #endregion

        #region Edit

        public void EditUser(Guid id, string name, bool enabled, bool locked, IEnumerable<Guid> roles)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.First(u => u.UserId == id);
                user.Name = name;
                user.Enabled = enabled;
                user.Locked = locked;
                user.UserRoles.Clear();
                ctx.SaveChanges();

                foreach (var roleId in roles)
                {
                    var role = ctx.Roles.Find(roleId);
                    user.UserRoles.Add(new UserRole()
                    {
                        RoleId = role.RoleId,
                        UserId = user.UserId,
                    });
                }
                ctx.SaveChanges();
            }
        }

        #endregion

        #region Delete

        public void DeleteUser(Guid id)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.First(u => u.UserId == id);
                ctx.Users.Remove(user);
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}