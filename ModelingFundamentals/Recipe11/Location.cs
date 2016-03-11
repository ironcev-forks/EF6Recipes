using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe11
{
    [Table("Locations", Schema = "Chapter2")]
    public class Location
    {
        public Location()
        {
            this.Parks = new HashSet<Park>();
        }

        public int LocationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIPCode { get; set; }

        public virtual ICollection<Park> Parks { get; set; }
    }
}
