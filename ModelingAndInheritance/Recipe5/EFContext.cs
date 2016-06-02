using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ModelingAndInheritance.Recipe5
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }

        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                        .HasMany(p => p.SubCategories)
                        .WithOptional(p => p.ParentCategory)
                        .Map(m =>m.MapKey("ParentCategoryId"));
            modelBuilder.Entity<Category>().ToTable("Categories", "chapter6");
           
            base.OnModelCreating(modelBuilder);
        }
        public ICollection<Category> GetSubCategories(int categoryId)
        {
            return Database.SqlQuery<Category>("exec chapter6.GetSubCategories @catId"
                , new SqlParameter[] { new SqlParameter("@catId", categoryId) }).ToList();
        }

    }
}
