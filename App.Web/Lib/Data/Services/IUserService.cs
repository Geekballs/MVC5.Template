using System;
using System.Collections.Generic;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        void Create(string userName, string firstName, string lastName, string alias, string emailAddress, bool loginEnabled, IEnumerable<Guid> roles, IEnumerable<Guid> teams);
        void Edit(Guid id, string userName, string firstName, string lastName, string alias, string emailAddress, bool loginEnabled, IEnumerable<Guid> roles, IEnumerable<Guid> teams);
        void Delete(Guid id);
        void Save();

        IEnumerable<UserRole> GetRolesForUser(Guid id);
        IEnumerable<UserTeam> GetTeamsForUser(Guid id);
    }
}
