using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelingAndInheritance.Recipe6
{
    /// <summary>
    ///6-6  映射派生类中的NULL条件
    /// </summary>
    public class RecipeProgram
    {
        public static void Run()
        {

            using (var context = new EFContext())
            {
                var exDrug1 = new Experimental
                {
                    Name = "Nanoxol",
                    PrincipalResearcher = "Dr. Susan James"
                };
                var exDrug2 = new Experimental
                {
                    Name = "Percosol",
                    PrincipalResearcher = "Dr. Bill Minor"
                };
                context.Drugs.Add(exDrug1);
                context.Drugs.Add(exDrug2);
                context.SaveChanges();

                // Nanoxol just got approved!
                exDrug1.PromoteToMedicine(DateTime.Now, 19.99M, "Treatall");
                context.Entry(exDrug1).State = EntityState.Detached; // better not use this instance any longer
            }

            using (var context = new EFContext())
            {
                Console.WriteLine("Experimental Drugs");
                foreach (var d in context.Drugs.OfType<Experimental>())
                {
                    Console.WriteLine("\t{0} ({1})", d.Name, d.PrincipalResearcher);
                }

                Console.WriteLine("Medicines");
                foreach (var d in context.Drugs.OfType<Medicine>())
                {
                    Console.WriteLine("\t{0} Retails for {1}", d.Name,
                                       d.TargetPrice.Value.ToString("C"));
                }
            }
        }

    }
}
