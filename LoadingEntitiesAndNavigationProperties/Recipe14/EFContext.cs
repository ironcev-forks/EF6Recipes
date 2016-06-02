using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe14
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Chapter5.Clients");
            modelBuilder.Entity<Invoice>().ToTable("Chapter5.Invoices");
        }
    }
}
