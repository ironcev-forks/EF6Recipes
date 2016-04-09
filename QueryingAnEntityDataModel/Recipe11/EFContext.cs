using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe11
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }
     
        public DbSet<Media> Medias { get; set; }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>().ToTable("Chapter3.Mediass")
                .Map<Article>(x => x.Requires("MediaType").HasValue(1))
                .Map<Picture>(x => x.Requires("MediaType").HasValue(2))
                .Map<Video>(x => x.Requires("MediaType").HasValue(3));
         
            base.OnModelCreating(modelBuilder);
        }
    }
}
