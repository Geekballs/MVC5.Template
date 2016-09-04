using App.Web.Lib.Data.Contexts;

namespace App.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AppDbContext context)
        {
        }
    }
}
