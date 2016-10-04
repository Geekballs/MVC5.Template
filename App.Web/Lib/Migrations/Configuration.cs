using App.Web.Lib.Data.Contexts;

namespace App.Web.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AppDbContext ctx)
        {
            SeedData.DefaultRoles(ctx);
            SeedData.TestUsers(ctx);
        }
    }
}
