using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe14
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<AssociateSalary> AssociateSalaries { get; set; }
        public DbSet<Associate> Associates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssociateSalary>().ToTable("Chapter3.AssociateSalaries");
            modelBuilder.Entity<Associate>().ToTable("Chapter3.Associates");
            base.OnModelCreating(modelBuilder);
        }
    }
}
