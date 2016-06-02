using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LoadingEntitiesAndNavigationProperties.Recipe6
{
    public class Plumber:Tradesman
    {
        public bool IsCertified { get; set; }
        public int LocationId { get; set; }
        public virtual JobSite JobSite { get; set; }
    }
}