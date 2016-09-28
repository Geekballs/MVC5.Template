using System;
using System.Collections.Generic;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    public interface ITeamService
    {
        IEnumerable<Team> GetAll();
        Team GetById(Guid id);
        void Create(string name, string description);
        void Edit(Guid id, string name, string description);
        void Delete(Guid id);
        void Save();

        IEnumerable<UserTeam> GetUsersInTeam(Guid id);
    }
}
