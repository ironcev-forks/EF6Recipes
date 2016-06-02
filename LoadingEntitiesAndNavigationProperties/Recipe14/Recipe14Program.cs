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

namespace LoadingEntitiesAndNavigationProperties.Recipe14
{
    /// <summary>
    ///5-14  修改外键关联
    /// </summary>
    public class Recipe14Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var client1 = new Client { Name = "Karen Standfield", ClientId = 1 };
                var invoice1 = new Invoice { InvoiceDate = DateTime.Parse("4/1/10"), Amount = 29.95M };
                var invoice2 = new Invoice { InvoiceDate = DateTime.Parse("4/2/10"), Amount = 49.95M };
                var invoice3 = new Invoice { InvoiceDate = DateTime.Parse("4/3/10"), Amount = 102.95M };
                var invoice4 = new Invoice { InvoiceDate = DateTime.Parse("4/4/10"), Amount = 45.99M };

                // 添加一个invoice到client的导航属性集合Invoices
                client1.Invoices.Add(invoice1);

                //直接分配一个外键值
                invoice2.ClientId = 1;

                //使用一个“假”实体附加一存在的行
                context.Database.ExecuteSqlCommand("insert into chapter5.clients values (2, 'Phil Marlowe')");
                var client2 = new Client { ClientId = 2 };
                context.Clients.Attach(client2);
                invoice3.Client = client2;

                // 使用Client引用
                invoice4.Client = client1;

                //保存更改
                context.Clients.Add(client1);
                context.Invoices.Add(invoice2);
                context.Invoices.Add(invoice3);
                context.Invoices.Add(invoice4);
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                foreach (var client in context.Clients)
                {
                    Console.WriteLine("Client: {0}", client.Name);
                    foreach (var invoice in client.Invoices)
                    {
                        Console.WriteLine("\t{0} for {1}", invoice.InvoiceDate.ToShortDateString(),
                                          invoice.Amount.ToString("C"));
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
