using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelingAndInheritance.Recipe9
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }

        public DbSet<Toy> Toys { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Toy>()
                        .Map(m => m.Requires("ForDonationOnly").HasValue(0))
                        .ToTable("Toys", "chapter6");
            modelBuilder.Entity<RefurbishedToy>()
                      .ToTable("RefurbishedToys", "chapter6");
            base.OnModelCreating(modelBuilder);
        }
    }
}
