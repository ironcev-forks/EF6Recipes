using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe11
{
    public class Contractor
    {
        public int ContractorID { get; set; }
        public string Name { get; set; }
        public int ProjectID { get; set; }

        public virtual Project Project { get; set; }
    }

}
