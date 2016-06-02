using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe6
{
    [Table("Drugs",Schema ="chapter6")]
    public abstract class Drug
    {
        public int DrugId { get; set; }
        public string Name { get; set; }
    }
}
