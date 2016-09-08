using System.Data.Entity.Migrations;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Migrations
{
    public static class SeedData
    {
        public static void DefaultRoles(AppDbContext ctx)
        {
            ctx.Roles.AddOrUpdate(x => x.Name,
                new Role() { Name = "Admin", Description = "Has access to some restricted features.", Enabled = true, Locked = true},
                new Role() { Name = "Kiosk", Description = "Has access public dashboards.", Enabled = true, Locked = true },
                new Role() { Name = "Super Admin", Description = "Has access to everything.", Enabled = true, Locked = true}
            );

            ctx.SaveChanges();
        }

        public static void TestUsers(AppDbContext ctx)
        {
            ctx.Users.AddOrUpdate(x => x.UserName,
                new User() { UserName = "Homer1", FirstName = "Homer", LastName = "Simpson", Enabled = true, Locked = true },
                new User() { UserName = "Marge1", FirstName = "Marge", LastName = "Simpson", Enabled = true, Locked = true },
                new User() { UserName = "Bart1", FirstName = "Bart", LastName = "Simpson", Enabled = true, Locked = true },
                new User() { UserName = "Lias1", FirstName = "Lisa", LastName = "Simpson", Enabled = true, Locked = true },
                new User() { UserName = "Maggie1", FirstName = "Maggie", LastName = "Simpson", Enabled = true, Locked = true }
            );

            ctx.SaveChanges();
        }
    }
}