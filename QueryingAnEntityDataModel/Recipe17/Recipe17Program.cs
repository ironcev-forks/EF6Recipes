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

namespace QueryingAnEntityDataModel.Recipe17
{
    /// <summary>
    /// 3-17  多列连接（Join)
    /// </summary>
    public class Recipe17Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                //删除之前的测试数据
                context.Database.ExecuteSqlCommand("delete from chapter3.orders");
                context.Database.ExecuteSqlCommand("delete from chapter3.accounts");
                //添加新的测试数据
                var account1 = new Account { City = "Raytown", State = "MO" };
                account1.Orders.Add(new Order
                {
                    Amount = 223.09M,
                    ShipCity = "Raytown",
                    ShipState = "MO"
                });
                account1.Orders.Add(new Order
                {
                    Amount = 189.32M,
                    ShipCity = "Olathe",
                    ShipState = "KS"
                });

                var account2 = new Account { City = "Kansas City", State = "MO" };
                account2.Orders.Add(new Order
                {
                    Amount = 99.29M,
                    ShipCity = "Kansas City",
                    ShipState = "MO"
                });

                var account3 = new Account { City = "North Kansas City", State = "MO" };
                account3.Orders.Add(new Order
                {
                    Amount = 102.29M,
                    ShipCity = "Overland Park",
                    ShipState = "KS"
                });
                context.Accounts.Add(account1);
                context.Accounts.Add(account2);
                context.Accounts.Add(account3);
                context.SaveChanges();

            }
            using (var context = new EFContext())
            {

                //var orders = from o in context.Orders
                //             join a in context.Accounts on
                //             new { Id = o.AccountID, City = o.ShipCity, State = o.ShipState }
                //             equals
                //             new { Id = a.AccountID, City = a.City, State = a.State }
                //             select o;
                var orders = context.Orders.Join(context.Accounts,
                    o => new { Id = o.AccountID, City = o.ShipCity, State = o.ShipState },
                    a => new { Id = a.AccountID, City = a.City, State = a.State },
                    (o, a) => o);
                Console.WriteLine("Orders shipped to the account's city, state...");
                foreach (var order in orders)
                {
                    Console.WriteLine("\tOrder {0} for {1}", order.AccountID.ToString(),
                    order.Amount.ToString());
                }

            }
           
        }

    }
}
