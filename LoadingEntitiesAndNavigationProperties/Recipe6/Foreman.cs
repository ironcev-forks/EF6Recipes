using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingEntitiesAndNavigationProperties.Recipe6
{
    public class Foreman
    {
        public int ForemanId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public virtual JobSite JobSite { get; set; }
    }
}
