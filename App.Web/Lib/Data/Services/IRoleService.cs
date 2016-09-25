using System;
using System.Collections.Generic;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(Guid id);
        void Create(string name, string description);
        void Edit(Guid id, string name, string description);
        void Delete(Guid id);
        void Save();

        IEnumerable<UserRole> GetUsersInRole(Guid id);
    }
}
