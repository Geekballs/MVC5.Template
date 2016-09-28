using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    public class DepartmentService : IDepartmentService, IDisposable
    {
        private readonly AppDbContext _ctx;

        public DepartmentService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Department> GetAll()
        {
            var depts = _ctx.Departments.Include(t => t.Teams).OrderBy(p => p.Name).ToList();
            return depts;
        }

        public Department GetById(Guid id)
        {
            var dept = _ctx.Departments.Include(t => t.Teams).First(p => p.DepartmentId == id);
            return dept;
        }

        public void Create(string name, string description)
        {
            var dept = new Department
            {
                Name = name,
                Description = description
            };
            _ctx.Departments.Add(dept);
            _ctx.SaveChanges();
        }

        public void Edit(Guid id, string name, string description)
        {
            var dept = _ctx.Departments.First(p => p.DepartmentId == id);
            dept.Name = name;
            dept.Description = description;
            _ctx.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var dept = _ctx.Departments.First(p => p.DepartmentId == id);
            _ctx.Departments.Remove(dept);
            _ctx.SaveChanges();
        }

        public IEnumerable<Team> GetTeamsInDepartment(Guid id)
        {
            var deptTeams = _ctx.Teams.Include(t => t.Department).Where(p => p.DepartmentId == id).ToList();
            return deptTeams;
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