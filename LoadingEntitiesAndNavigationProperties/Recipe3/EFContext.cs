using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe3
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }

        public DbSet<Club> Clubs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>().ToTable("Chapter5.Clubs");
            base.OnModelCreating(modelBuilder);
        }
    }
}
