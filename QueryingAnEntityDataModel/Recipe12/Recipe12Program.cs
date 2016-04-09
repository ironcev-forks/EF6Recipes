using QueryingAnEntityDataModel.Recipe5;
using QueryingAnEntityDataModel.Recipe7;
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

namespace QueryingAnEntityDataModel.Recipe12
{
    /// <summary>
    /// 3-12 分页和过滤
    /// </summary>
    public class Recipe12Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                // delete previous test data
                context.Database.ExecuteSqlCommand("delete from chapter3.customers");
                // add new test data
                context.Customers.Add(new Customer
                {
                    Name = "Roberts, Jill",
                    Email = " jroberts@abc.com"
                });
                context.Customers.Add(new Customer
                {
                    Name = "Robertson, Alice",
                    Email = " arob@gmail.com"
                });
                context.Customers.Add(new Customer
                {
                    Name = "Rogers, Steven",
                    Email = " srogers@termite.com"
                });
                context.Customers.Add(new Customer
                {
                    Name = "Roe, Allen",
                    Email = " allenr@umc.com"
                });
                context.Customers.Add(new Customer
                {
                    Name = "Jones, Chris",
                    Email = " cjones@ibp.com"
                });
                context.SaveChanges();
            }
            using (var context = new EFContext())
            {
                string match = "Ro";
                int pageIndex = 0;
                int pageSize = 3;
                var customers = context.Customers.Where(p => p.Name.StartsWith(match))
                    .OrderBy(p => p.Name)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
                Console.WriteLine("Customers Ro*");
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0} [email: {1}]", customer.Name, customer.Email);
                }
            }
            using (var context = new EFContext())
            {
                string match = "Ro%";
                int pageIndex = 0;
                int pageSize = 3;

                var esql = @"select value c from Customers as c 
                 where c.Name like @Name
                 order by c.Name
                 skip @Skip limit @Limit";
                Console.WriteLine("\nCustomers Ro*");
                var customers = ((IObjectContextAdapter)context).ObjectContext.CreateQuery<Customer>(esql, new[]
                      {
                        new ObjectParameter("Name",match),
                        new ObjectParameter("Skip",pageIndex * pageSize),
                        new ObjectParameter("Limit",pageSize)
                      });
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0} [email: {1}]", customer.Name, customer.Email);
                }
            }
        }

    }
}
