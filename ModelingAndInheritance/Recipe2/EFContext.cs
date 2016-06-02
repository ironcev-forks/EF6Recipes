using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe2
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFconnectionString") { }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<WorkerTask> WorkerTasks { get; set; }
       
    }
}
