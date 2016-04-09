using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe17
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Chapter3.Accounts");
            modelBuilder.Entity<Order>().ToTable("Chapter3.Orders");
            base.OnModelCreating(modelBuilder);
        }
    }
}
