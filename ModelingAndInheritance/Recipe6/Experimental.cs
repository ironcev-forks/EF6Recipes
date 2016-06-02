using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe6
{
    public class Experimental:Drug
    {
        public string PrincipalResearcher { get; set; }
        public void PromoteToMedicine(DateTime acceptedDate, decimal targetPrice,string marketingName)
        {
            var drug = new Medicine { DrugId = this.DrugId };
            using (var context = new EFContext())
            {
                context.Drugs.Attach(drug);
                drug.AcceptedDate = acceptedDate;
                drug.TargetPrice = targetPrice;
                drug.Name = marketingName;
                context.SaveChanges();
            }
        }

    }
}
