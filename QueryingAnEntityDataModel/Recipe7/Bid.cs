using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe7
{
    public class Bid
    {
        public int BidId { get; set; }
        public int JobId { get; set; }
        public decimal Amount { get; set; }
        public string Bidder { get; set; }
        public virtual Job Job { get; set; }
    }
}
