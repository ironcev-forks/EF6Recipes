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

namespace LoadingEntitiesAndNavigationProperties.Recipe1
{
    /// <summary>
    /// 5-1 延迟加载关联实体
    /// </summary>
    public class Recipe1Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var web = new CustomerType { Description = "Web Customer", CustomerTypeId = 1 };
                var retail = new CustomerType { Description = "Retail Customer", CustomerTypeId = 2 };
                var customer = new Customer { Name = "Joan Smith", CustomerType = web };
                customer.CustomerEmails.Add(new CustomerEmail { Email = "jsmith@gmail.com" });
                customer.CustomerEmails.Add(new CustomerEmail { Email = "joan@smith.com" });
                context.Customers.Add(customer);
                customer = new Customer { Name = "Bill Meyers", CustomerType = retail };
                customer.CustomerEmails.Add(new CustomerEmail { Email = "bmeyers@gmail.com" });
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            using (var context = new EFContext())
            {
                var customers = context.Customers;
                Console.WriteLine("Customers");
                Console.WriteLine("=========");

                // 只需要Customer实体的信息
                foreach (var customer in customers)
                {
                    Console.WriteLine("Customer name is {0}", customer.Name);
                }


                //现在，需要使用到关联实体CustomerType和CustomerEamil的信息，
                //实体框架为每个实体对象产生一个单独的查询来获取关联实体的信息。
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0} is a {1}, email address(es)", customer.Name,
                                      customer.CustomerType.Description);
                    foreach (var email in customer.CustomerEmails)
                    {
                        Console.WriteLine("\t{0}", email.Email);
                    }
                }

                // Extra credit:
                //如果你打开Sql Server Profiler，下面的查询将不会重新去数据库查询，而是返回之前查询的内存数据
                //注：原书中的注释有些模糊，本行是我（付灿）增加的说明，这里还要去查询Customer表一次，但是它的关联属性，不会再去查询数据库了，而是直接从内存返回之前查询出来的对象
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0} is a {1}, email address(es)", customer.Name,
                                      customer.CustomerType.Description);
                    foreach (var email in customer.CustomerEmails)
                    {
                        Console.WriteLine("\t{0}", email.Email);
                    }
                }
            }


        }
    }
}
