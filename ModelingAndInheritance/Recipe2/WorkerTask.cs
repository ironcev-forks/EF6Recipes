using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelingAndInheritance.Recipe2
{
    [Table("WorkerTasks",Schema ="chapter6")]
    public class WorkerTask
    {
        [Key]
        [Column(Order =1)]
        public int WorkerId { get; set; }
        [Key]
        [Column(Order =2)]
        public int TaskId { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual Task Task { get; set; }
    }
}