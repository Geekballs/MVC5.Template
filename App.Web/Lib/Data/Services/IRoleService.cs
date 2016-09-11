using System;
using System.Collections.Generic;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();
        Role GetById(Guid roleId);
        IEnumerable<UserRole> GetUsersInRole(Guid roleId);
        void CreateRole(string name, string description);
        void EditRole(Guid id, string name, string description);
        void DeleteRole(Guid roleId);
        void Save();
    }
}
