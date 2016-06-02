using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe1
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Event> Events { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Chapter6.Events");
            modelBuilder.Entity<Organizer>().ToTable("Chapter6.Organizers");
            base.OnModelCreating(modelBuilder);
        }
    }
}
