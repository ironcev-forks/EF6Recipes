using LoadingEntitiesAndNavigationProperties.Recipe1;
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

namespace LoadingEntitiesAndNavigationProperties.Recipe2
{
    /// <summary>
    /// 5-2 预先加载关联实体
    /// </summary>
    public class Recipe2Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {

                //Include()方法，使用基于字符串类型的，与导航属性相对应的查询路径
                var customers = context.Customers
                                       .Include("CustomerType")
                                       .Include("CustomerEmails");
                Console.WriteLine("Customers");
                Console.WriteLine("=========");
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

            using (var context = new EFContext())
            {
                //Include()方法，使用基于强类型的，与导航属性相对应的查询路径
                var customerTypes = context.CustomerTypes
                                           .Include(x => x.Customers
                                                          .Select(y => y.CustomerEmails));

                Console.WriteLine("\nCustomers by Type");
                Console.WriteLine("=================");
                foreach (var customerType in customerTypes)
                {
                    Console.WriteLine("Customer type: {0}", customerType.Description);
                    foreach (var customer in customerType.Customers)
                    {
                        Console.WriteLine("{0}", customer.Name);
                        foreach (var email in customer.CustomerEmails)
                        {
                            Console.WriteLine("\t{0}", email.Email);
                        }
                    }
                }
            }

        }
    }
}
