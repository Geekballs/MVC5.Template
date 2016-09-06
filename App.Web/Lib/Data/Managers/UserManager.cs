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
                var users = ctx.Users.Include(x => x.UserRoles).OrderBy(x => x.Name).ToList();
                return users;
            }
        }

        public User GetUserById(Guid? id)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.Include(x => x.UserRoles).First(x => x.UserId == id);
                return user;
            }
        }

        public List<UserRole> GetRolesForUser(Guid? id)
        {
            using (var ctx = new AppDbContext())
            {
                var roles = ctx.UserRoles.Include(x => x.Role).Where(x => x.UserId == id).ToList();
                return roles;
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

        #region Update

        public void UpdateUser(Guid id, string name, bool enabled, bool locked, IEnumerable<Guid> roles)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.First(x => x.UserId == id);

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
                var user = ctx.Users.First(x => x.UserId == id);
                ctx.Users.Remove(user);
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}