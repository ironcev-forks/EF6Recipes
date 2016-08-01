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

namespace ModelingAndInheritance.Recipe9
{
    /// <summary>
    ///6-8  嵌套的TPH建模
    /// </summary>
    public class RecipeProgram
    {
        public static void Run()
        {

            using (var context = new EFContext())
            {
                //context.Database.ExecuteSqlCommand(@"insert into chapter6.toys(Name,ForDonationOnly) values ('RagDoll',1)");
                var toy = new Toy { Name = "Fuzzy Bear", Price = 9.97M };
                var refurb = new RefurbishedToy
                {
                    Name = "Derby Car",
                    Price = 19.99M,
                    Quality = "Ok to sell"
                };
                context.Toys.Add(toy);
                //context.Toys.Add(refurb);
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                Console.WriteLine("All Toys");
                Console.WriteLine("========");
                foreach (var toy in context.Toys)
                {
                    Console.WriteLine("{0}", toy.Name);
                }
                Console.WriteLine("\nRefurbished Toys");
                foreach (var toy in context.Toys.OfType<RefurbishedToy>())
                {
                    Console.WriteLine("{0}, Price = {1}, Quality = {2}", toy.Name,
                                       toy.Price, ((RefurbishedToy)toy).Quality);
                }
            }
        }
    }
}
