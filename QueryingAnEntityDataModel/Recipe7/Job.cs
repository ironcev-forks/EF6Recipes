using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe7
{
    public class Job
    {
        public Job()
        {
            Bids = new HashSet<Bid>();
        }
        public int JobId { get; set; }
        public string JobDetails { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
