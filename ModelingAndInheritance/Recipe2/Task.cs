using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe2
{
    [Table("Tasks",Schema ="chapter6")]
    public class Task
    {
        public int TaskId { get; set; }
        [Column("Name")]
        public string Title { get; set; }
        public virtual ICollection<WorkerTask> WorkerTasks { get; set; }
    }
}
