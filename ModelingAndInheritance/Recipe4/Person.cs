using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelingAndInheritance.Recipe4
{
    [Table("Persons",Schema ="chapter6")]
    public abstract class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public virtual Person Hero { get; set; }
        public virtual ICollection<Person> Fans { get; set; }
    }
    public class Firefighter : Person
    {
        public string FireStation { get; set; }
    }

    public class Teacher : Person
    {
        public string School { get; set; }
    }

    public class Retired : Person
    {
        public string FullTimeHobby { get; set; }
    }
}
