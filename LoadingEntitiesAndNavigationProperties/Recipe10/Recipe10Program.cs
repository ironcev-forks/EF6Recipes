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

namespace LoadingEntitiesAndNavigationProperties.Recipe10
{
    /// <summary>
    ///5-10  在关联实体上执行聚合操作
    /// </summary>
    public class Recipe10Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                var order = new Order { CustomerName = "Jenny Craig", OrderDate = DateTime.Parse("3/12/2010") };

                var item1 = new OrderItem { Order = order, Shipped = 3, SKU = 2827, UnitPrice = 12.95M };
                var item2 = new OrderItem { Order = order, Shipped = 1, SKU = 1918, UnitPrice = 19.95M };
                var item3 = new OrderItem { Order = order, Shipped = 3, SKU = 392, UnitPrice = 8.95M };

                order.OrderItems.Add(item1);
                order.OrderItems.Add(item2);
                order.OrderItems.Add(item3);

                context.Orders.Add(order);
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                //为订单计算总价的一种方法是，使用Load()方法加载订单项的整个集合，然后枚举它并对订单项进行求合计算。
                //另一种方法是，将计算的过程放到数据库中去，让它完成计算后返回。
                //第二方法的优点是，它避免了为实现这个唯一的目标而实例化每个订单项的潜在成本。代码清单5 - 26演示了这种方法
                  // 假设我们有一个Order实体
                  var order = context.Orders.First();

                // 获取订单总价
                var amt = context.Entry(order)
                    .Collection(x => x.OrderItems)
                    .Query()
                    .Sum(y => y.Shipped * y.UnitPrice);

                Console.WriteLine("Order Number: {0}", order.OrderId);
                Console.WriteLine("Order Date: {0}", order.OrderDate.ToShortDateString());
                Console.WriteLine("Order Total: {0}", amt.ToString("C"));
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
