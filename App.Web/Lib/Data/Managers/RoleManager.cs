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
                var roles = ctx.Roles.Include(x => x.UserRoles).OrderBy(x => x.Name).ToList();
                return roles;
            }
        }

        public Role GetRoleById(Guid? id)
        {
            using (var ctx = new AppDbContext())
            {
                var role = ctx.Roles.Include(x => x.UserRoles).First(x => x.RoleId == id);
                return role;
            }
        }

        public List<UserRole> GeUsersInRole(Guid? id)
        {
            using (var ctx = new AppDbContext())
            {
                var users = ctx.UserRoles.Include(x => x.User).Where(x => x.RoleId == id).ToList();
                return users;
            }
        }

        #endregion

        #region Create

        public void CreateRole(string name, string description)
        {
            using (var ctx = new AppDbContext())
            {
                var role = new Role()
                {
                    Name = name,
                    Description = description,
                    Enabled = true,
                    Locked = true
                };
                ctx.Roles.Add(role);
                ctx.SaveChanges();
            }
        }

        #endregion

        #region Update

        public void UpdateRole(Guid id, string name, string description, bool enabled, bool locked)
        {
            using (var ctx = new AppDbContext())
            {
                var role = ctx.Roles.First(x => x.RoleId == id);
                role.Name = name;
                role.Description = description;
                role.Enabled = enabled;
                role.Locked = locked;
                role.UserRoles.Clear();
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