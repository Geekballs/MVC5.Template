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

        public IEnumerable<User> GetAll()
        {
            var users = _ctx.Users.Include(t => t.UserRoles).OrderBy(p => p.UserName).ToList();
            return users;
        }

        public User GetById(Guid id)
        {
            var user = _ctx.Users.Include(t => t.UserRoles).First(p => p.UserId == id);
            return user;
        }

        public void Create(string userName, string firstName, string lastName, string alias, string emailAddress, bool loginEnabled, IEnumerable<Guid> roles, IEnumerable<Guid> teams)
        {
            var user = new User
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                Alias = alias,
                EmailAddress = emailAddress,
                LoginEnabled = loginEnabled
            };

            foreach (var roleId in roles)
            {
                var role = _ctx.Roles.Find(roleId);
                user.UserRoles.Add(new UserRole
                {
                    RoleId = role.RoleId,
                    UserId = user.UserId
                });
            }
            foreach (var teamId in teams)
            {
                var team = _ctx.Teams.Find(teamId);
                user.UserTeams.Add(new UserTeam
                {
                    TeamId = team.TeamId,
                    UserId = user.UserId
                });
            }
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
        }

        public void Edit(Guid id, string userName, string firstName, string lastName, string alias, string emailAddress, bool loginEnabled, IEnumerable<Guid> roles, IEnumerable<Guid> teams)
        {
            var user = _ctx.Users.First(p => p.UserId == id);
            user.UserName = userName;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Alias = alias;
            user.EmailAddress = emailAddress;
            user.LoginEnabled = loginEnabled;
            user.UserRoles.Clear();
            user.UserTeams.Clear();
            _ctx.SaveChanges();

            foreach (var roleId in roles)
            {
                var role = _ctx.Roles.Find(roleId);
                user.UserRoles.Add(new UserRole
                {
                    RoleId = role.RoleId,
                    UserId = user.UserId,
                });
            }
            foreach (var teamId in teams)
            {
                var team = _ctx.Teams.Find(teamId);
                user.UserTeams.Add(new UserTeam
                {
                    TeamId = team.TeamId,
                    UserId = user.UserId,
                });
            }
            _ctx.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = _ctx.Users.First(p => p.UserId == id);
            _ctx.Users.Remove(user);
            _ctx.SaveChanges();
        }

        public IEnumerable<UserRole> GetRolesForUser(Guid id)
        {
            var userRoles = _ctx.UserRoles.Include(t => t.Role).Where(p => p.UserId == id).ToList();
            return userRoles;
        }

        public IEnumerable<UserTeam> GetTeamsForUser(Guid id)
        {
            var userTeams = _ctx.UserTeams.Include(t => t.Team).Where(p => p.UserId == id).ToList();
            return userTeams;
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