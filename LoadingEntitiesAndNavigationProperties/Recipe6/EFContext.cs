using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe6
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Tradesman> Tradesmen { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Foreman> Foremen { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tradesman>().ToTable("Chapter5.Tradesmen");
            modelBuilder.Entity<Location>().ToTable("Chapter5.Locations");
            modelBuilder.Entity<Phone>().ToTable("Chapter5.Phones");
            modelBuilder.Entity<Foreman>().ToTable("Chapter5.Foremen");
            base.OnModelCreating(modelBuilder);
        }
    }
}
