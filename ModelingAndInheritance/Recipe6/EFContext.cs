using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ModelingAndInheritance.Recipe6
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }

        public DbSet<Drug> Drugs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicine>().Map(p => p.Requires(d => d.AcceptedDate).HasValue());
            modelBuilder.Entity<Experimental>().Map(p => p.Requires("AcceptedDate").HasValue((DateTime?)null));
            base.OnModelCreating(modelBuilder);
        }
      

    }
}
