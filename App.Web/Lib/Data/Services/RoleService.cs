using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    public class RoleService : IRoleService, IDisposable
    {
        private readonly AppDbContext _ctx;

        public RoleService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var roles = _ctx.Roles.Include(ur => ur.UserRoles).OrderBy(u => u.Name).ToList();
            return roles;
        }

        public Role GetById(Guid roleId)
        {
            var role = _ctx.Roles.Include(ur => ur.UserRoles).First(r => r.RoleId == roleId);
            return role;
        }

        public IEnumerable<UserRole> GetUsersInRole(Guid roleId)
        {
            var roleUsers = _ctx.UserRoles.Include(u => u.User).Where(ur => ur.RoleId == roleId).OrderBy(u => u.User.UserName).ToList();
            return roleUsers;
        }

        public void CreateRole(string name, string description)
        {
            var role = new Role()
            {
                Name = name,
                Description = description
            };
            _ctx.Roles.Add(role);
            _ctx.SaveChanges();
        }

        public void EditRole(Guid id, string name, string description)
        {
            var role = _ctx.Roles.First(r => r.RoleId == id);
            role.Name = name;
            role.Description = description;
            _ctx.SaveChanges();
        }

        public void DeleteRole(Guid roleId)
        {
            var role = _ctx.Roles.First(x => x.RoleId == roleId);
            _ctx.Roles.Remove(role);
            _ctx.SaveChanges();
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}