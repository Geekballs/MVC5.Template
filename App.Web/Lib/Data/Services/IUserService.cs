using System;
using System.Collections.Generic;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetById(Guid userId);
        User GetByName(string name);
        IEnumerable<UserRole> GetRolesForUser(Guid userId);
        void CreateUser(string name, bool enabled, bool locked, IEnumerable<Guid> roles);
        void EditUser(Guid userId, string name, bool enabled, bool locked, IEnumerable<Guid> roles);
        void DeleteUser(Guid userId);
        void Save();
    }
}
