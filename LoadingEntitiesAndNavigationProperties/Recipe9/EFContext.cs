using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe9
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().ToTable("Chapter5.Hotels");
            modelBuilder.Entity<Room>().ToTable("Chapter5.Rooms");
            modelBuilder.Entity<Reservation>().ToTable("Chapter5.Reservations");
            base.OnModelCreating(modelBuilder);
        }
    }
}
