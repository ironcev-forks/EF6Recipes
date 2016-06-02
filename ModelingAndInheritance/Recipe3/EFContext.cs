using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe3
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .HasMany(p => p.RelatedProducts)
                        .WithMany(p => p.OtherRelatedProducts)
                        .Map(m =>
                        {
                            m.MapLeftKey("ProductId");
                            m.MapRightKey("RelatedProductId");
                            m.ToTable("RelatedProducts", "Chapter6");
                        });
            base.OnModelCreating(modelBuilder);
        }

    }
}
