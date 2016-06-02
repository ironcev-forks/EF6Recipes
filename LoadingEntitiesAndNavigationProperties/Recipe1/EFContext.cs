using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe1
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerEmail> CustomerEmails { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Chapter5.Customers");
            modelBuilder.Entity<CustomerType>().ToTable("Chapter5.CustomerTypes");
            modelBuilder.Entity<CustomerEmail>().ToTable("Chapter5.CustomerEmails");
            base.OnModelCreating(modelBuilder);
        }
    }
}
