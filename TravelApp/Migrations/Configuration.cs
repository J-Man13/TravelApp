namespace TravelApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TravelApp.Models.EntityModels.LocalTravelAppMSSQLDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TravelApp.Models.EntityModels.LocalTravelAppMSSQLDBContext";
        }

        protected override void Seed(TravelApp.Models.EntityModels.LocalTravelAppMSSQLDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
