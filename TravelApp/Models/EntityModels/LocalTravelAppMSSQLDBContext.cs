using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models.EntityModels
{
    public class LocalTravelAppMSSQLDBContext : DbContext
    {
        public LocalTravelAppMSSQLDBContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LocalTravelAppMSSQLDBContext, TravelApp.Migrations.Configuration>());
        }


        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<TripEntity> TripEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}