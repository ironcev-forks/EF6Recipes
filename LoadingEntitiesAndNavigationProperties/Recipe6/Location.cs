using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LoadingEntitiesAndNavigationProperties.Recipe6
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIPCode { get; set; }
        public int PhoneId { get; set; }
        public virtual Phone Phone { get; set; }
    }
}