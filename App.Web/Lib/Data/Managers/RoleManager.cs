using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Managers
{
    public class RoleManager
    {
        #region Get

        public IEnumerable<Role> GetAllRoles()
        {
            using (var ctx = new AppDbContext())
            {
                var roles = ctx.Roles.Include(ur => ur.UserRoles).OrderBy(u => u.Name).ToList();
                return roles;
            }
        }

        public Role GetRoleById(Guid? id)
        {
            using (var ctx = new AppDbContext())
            {
                var role = ctx.Roles.Include(ur => ur.UserRoles).First(r => r.RoleId == id);
                return role;
            }
        }

        public List<UserRole> GeUsersInRole(Guid? id)
        {
            using (var ctx = new AppDbContext())
            {
                var roleUsers = ctx.UserRoles.Include(u => u.User).Where(ur => ur.RoleId == id).OrderBy(u => u.User.Name).ToList();
                return roleUsers;
            }
        }

        #endregion

        #region Create

        public void CreateRole(string name, string description, bool enabled, bool locked)
        {
            using (var ctx = new AppDbContext())
            {
                var role = new Role()
                {
                    Name = name,
                    Description = description,
                    Enabled = enabled,
                    Locked = locked
                };
                ctx.Roles.Add(role);
                ctx.SaveChanges();
            }
        }

        #endregion

        #region Edit

        public void EditRole(Guid id, string name, string description, bool enabled, bool locked)
        {
            using (var ctx = new AppDbContext())
            {
                var role = ctx.Roles.First(r => r.RoleId == id);
                role.Name = name;
                role.Description = description;
                role.Enabled = enabled;
                role.Locked = locked;
                ctx.SaveChanges();
            }
        }

        #endregion

        #region Delete

        public void DeleteRole(Guid id)
        {
            using (var ctx = new AppDbContext())
            {
                var role = ctx.Roles.First(x => x.RoleId == id);
                ctx.Roles.Remove(role);
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}