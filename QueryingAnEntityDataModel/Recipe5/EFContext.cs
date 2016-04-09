using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe5
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>().ToTable("Chapter3.BlogPosts").HasKey(k=>k.PostId);
            modelBuilder.Entity<Comment>().ToTable("Chapter3.Comments");
            base.OnModelCreating(modelBuilder);
        }
    }
}
