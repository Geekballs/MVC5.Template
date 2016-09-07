using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    public class UserService : IUserService, IDisposable
    {
        private readonly AppDbContext _ctx;

        public UserService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _ctx.Users.Include(ur => ur.UserRoles).OrderBy(u => u.Name).ToList();
            return users;
        }

        public User GetById(Guid userId)
        {
            var user = _ctx.Users.Include(ur => ur.UserRoles).First(u => u.UserId == userId);
            return user;
        }

        public User GetByName(string name)
        {
            var user = _ctx.Users.Include(ur => ur.UserRoles).First(u => u.Name == name);
            return user;
        }

        public IEnumerable<UserRole> GetRolesForUser(Guid userId)
        {
            var userRoles = _ctx.UserRoles.Include(r => r.Role).Where(ur => ur.UserId == userId).ToList();
            return userRoles;
        }

        public void CreateUser(string name, bool enabled, bool locked, IEnumerable<Guid> roles)
        {
            var user = new User()
            {
                Name = name,
                Enabled = enabled,
                Locked = locked
            };
            foreach (var roleId in roles)
            {
                var role = _ctx.Roles.Find(roleId);
                user.UserRoles.Add(new UserRole()
                {
                    RoleId = role.RoleId,
                    UserId = user.UserId
                });
            }
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
        }

        public void EditUser(Guid id, string name, bool enabled, bool locked, IEnumerable<Guid> roles)
        {
            var user = _ctx.Users.First(u => u.UserId == id);
            user.Name = name;
            user.Enabled = enabled;
            user.Locked = locked;
            user.UserRoles.Clear();
            _ctx.SaveChanges();

            foreach (var roleId in roles)
            {
                var role = _ctx.Roles.Find(roleId);
                user.UserRoles.Add(new UserRole()
                {
                    RoleId = role.RoleId,
                    UserId = user.UserId,
                });
            }
            _ctx.SaveChanges();
        }

        public void DeleteUser(Guid userId)
        {
            var user = _ctx.Users.First(u => u.UserId == userId);
            _ctx.Users.Remove(user);
            _ctx.SaveChanges();
        }

        public bool UserTrust(string name, string accessToken)
        {
            var user = _ctx.UserRoles.Count(ur => ur.User.Name == name && ur.User.Enabled && ur.Role.Name == accessToken) > 0;
            return user;
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