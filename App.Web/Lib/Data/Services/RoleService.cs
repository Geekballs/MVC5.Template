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

        public IEnumerable<Role> GetAll()
        {
            var roles = _ctx.Roles.Include(t => t.UserRoles).OrderBy(p => p.Name).ToList();
            return roles;
        }

        public Role GetById(Guid id)
        {
            var role = _ctx.Roles.Include(t => t.UserRoles).First(p => p.RoleId == id);
            return role;
        }

        public void Create(string name, string description)
        {
            var role = new Role()
            {
                Name = name,
                Description = description
            };
            _ctx.Roles.Add(role);
            _ctx.SaveChanges();
        }

        public void Edit(Guid id, string name, string description)
        {
            var role = _ctx.Roles.First(p => p.RoleId == id);
            role.Name = name;
            role.Description = description;
            _ctx.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var role = _ctx.Roles.First(p => p.RoleId == id);
            _ctx.Roles.Remove(role);
            _ctx.SaveChanges();
        }

        public IEnumerable<UserRole> GetUsersInRole(Guid id)
        {
            var roleUsers = _ctx.UserRoles.Include(t => t.User).Where(p => p.RoleId == id).OrderBy(p => p.User.UserName).ToList();
            return roleUsers;
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