using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe1
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
        public DbSet<Associate> Associates { get; set; }
        public DbSet<AssociateSalary> AssociateSalaries { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Associate>().ToTable("Chapter3.Associate");
            modelBuilder.Entity<AssociateSalary>().ToTable("Chapter3.AssociateSalary");
            // Explicilty assign key as primary key in AssociateSalary does not meet
            // Entity Framework default mapping conventions.
            modelBuilder.Entity<AssociateSalary>().HasKey(x => x.SalaryId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
