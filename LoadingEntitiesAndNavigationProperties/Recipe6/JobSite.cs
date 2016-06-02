using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LoadingEntitiesAndNavigationProperties.Recipe6
{
    public class JobSite:Location
    {
        public JobSite()
        {
            Foremen = new HashSet<Foreman>();
            Plumbers = new HashSet<Plumber>();
        }
        public string JobSiteName { get; set; }
        public virtual ICollection<Foreman> Foremen { get; set; }
        public virtual ICollection<Plumber> Plumbers { get; set; }
    }
}