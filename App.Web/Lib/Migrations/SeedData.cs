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
                new Role() { Name = "Admin", Description = "Has access to some restricted features." },
                new Role() { Name = "Kiosk", Description = "Has access public dashboards." },
                new Role() { Name = "Super Admin", Description = "Has access to everything." }
            );

            ctx.SaveChanges();
        }

        public static void TestUsers(AppDbContext ctx)
        {
            ctx.Users.AddOrUpdate(x => x.UserName,
                new User() { UserName = "Homer1", FirstName = "Homer", LastName = "Simpson", Alias = "HomerAlias", EmailAddress = "test1@email.com", LoginEnabled = true },
                new User() { UserName = "Marge1", FirstName = "Marge", LastName = "Simpson", Alias = "MargeAlias", EmailAddress = "test2@email.com", LoginEnabled = false },
                new User() { UserName = "Bart1", FirstName = "Bart", LastName = "Simpson", Alias = "BartAlias", EmailAddress = "test3@email.com", LoginEnabled = true },
                new User() { UserName = "Lisa1", FirstName = "Lisa", LastName = "Simpson", Alias = "LisaAlias", EmailAddress = "test4@email.com", LoginEnabled = false },
                new User() { UserName = "Maggie1", FirstName = "Maggie", LastName = "Simpson", Alias = "MaggieAlias", EmailAddress = "test5@email.com", LoginEnabled = true }
            );

            ctx.SaveChanges();
        }
    }
}