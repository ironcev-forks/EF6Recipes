using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe4
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }

        public DbSet<Person> Persons { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                        .HasMany(p => p.Fans)
                        .WithOptional(p => p.Hero)
                        .Map(m =>m.MapKey("HeroId"));
            modelBuilder.Entity<Person>()
                        .Map<Teacher>(m => m.Requires("Role").HasValue("T"))
                        .Map<Firefighter>(m => m.Requires("Role").HasValue("F"))
                        .Map<Retired>(m => m.Requires("Role").HasValue("R")); 
            base.OnModelCreating(modelBuilder);
        }

    }
}
