using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingAnEntityDataModel.Recipe17
{
    public class Account
    {
        public Account()
        {
            Orders =new  HashSet<Order>();
        }
        public int AccountID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
