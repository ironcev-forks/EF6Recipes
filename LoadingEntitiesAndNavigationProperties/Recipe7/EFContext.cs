using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe7
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Event> Events { get; set; }
        public DbSet<Club> Clubs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Chapter5.Events");
            modelBuilder.Entity<Club>().ToTable("Chapter5.Clubs");
            base.OnModelCreating(modelBuilder);
        }
    }
}
