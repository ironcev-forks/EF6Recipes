using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe5
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
        public DbSet<PictureCategory> PictureCategories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //这里可以不要，因为entityframework可能根据实体模型的配置推断出相关关系。
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<PictureCategory>().HasMany(cat => cat.Subcategories).WithOptional(cat => cat.ParentCategory);
        }

    }
}
