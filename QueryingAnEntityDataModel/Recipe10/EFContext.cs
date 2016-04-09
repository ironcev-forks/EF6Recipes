using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe10
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Product> Products { get; set; }
        public DbSet<TopSelling> TopSellings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Chapter3.Products");
            modelBuilder.Entity<TopSelling>().ToTable("Chapter3.TopSellings");
            base.OnModelCreating(modelBuilder);
        }
    }
}
