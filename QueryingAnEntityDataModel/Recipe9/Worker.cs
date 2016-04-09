using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe9
{
    public class Worker
    {
        public Worker()
        {
            Accidents = new HashSet<Accident>();
        }

        public int WorkerId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Accident> Accidents { get; set; }
    }
}
