using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    public class TeamService : ITeamService, IDisposable
    {
        private readonly AppDbContext _ctx;

        public TeamService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Team> GetAll()
        {
            var teams = _ctx.Teams.Include(t => t.UserTeams).OrderBy(p => p.Name).ToList();
            return teams;
        }

        public Team GetById(Guid id)
        {
            var team = _ctx.Teams.Include(t => t.UserTeams).First(p => p.TeamId == id);
            return team;
        }

        public void Create(string name, string description)
        {
            var team = new Team
            {
                Name = name,
                Description = description
            };
            _ctx.Teams.Add(team);
            _ctx.SaveChanges();
        }

        public void Edit(Guid id, string name, string description)
        {
            var team = _ctx.Teams.First(p => p.TeamId == id);
            team.Name = name;
            team.Description = description;
            _ctx.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var team = _ctx.Teams.First(p => p.TeamId == id);
            _ctx.Teams.Remove(team);
            _ctx.SaveChanges();
        }

        public IEnumerable<UserTeam> GetUsersInTeam(Guid id)
        {
            var teamUsers = _ctx.UserTeams.Include(t => t.User).Where(p => p.TeamId == id).OrderBy(p => p.User.UserName).ToList();
            return teamUsers;
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