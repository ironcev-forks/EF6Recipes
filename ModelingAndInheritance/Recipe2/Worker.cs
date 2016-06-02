using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe2
{
    [Table("Workers",Schema ="chapter6")]
    public class Worker
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<WorkerTask> WorkerTasks { get; set; }
    }
}
