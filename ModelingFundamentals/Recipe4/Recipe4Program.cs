using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelingFundamentals.Recipe4
{
    /// <summary>
    /// 2-4 有载荷的多对多关系建模
    /// </summary>
    public class Recipe4Program
    {
        public static void  Run()
        {
            using (var context = new EFContext())
            {
                context.Database.ExecuteSqlCommand("delete from chapter2.OrderItems");
                context.Database.ExecuteSqlCommand("delete from chapter2.Orders");
                context.Database.ExecuteSqlCommand("delete from chapter2.Items");
                var order = new Order
                {
                    OrderId = 1,
                    OrderDate = new DateTime(2010, 1, 18)
                };
                var item = new Item
                {
                    SKU = 1729,
                    Description = "Backpack",
                    Price = 29.97M
                };
                var oi1 = new OrderItem { Order = order, Item = item, Count = 1 };
                item = new Item
                {
                    SKU = 2929,
                    Description = "Water Filter",
                    Price = 13.97M
                };
                var oi2 = new OrderItem { Order = order, Item = item, Count = 3 };
                item = new Item
                {
                    SKU = 1847,
                    Description = "Camp Stove",
                    Price = 43.99M
                };
                var oi3 = new OrderItem { Order = order, Item = item, Count = 1 };
                //context.Orders.Add(order);
                context.OrderItem.Add(oi1);
                context.OrderItem.Add(oi2);
                context.OrderItem.Add(oi3);
                context.SaveChanges();
            }
            using (var context = new EFContext())
            {
                foreach (var order in context.Orders)
                {
                    Console.WriteLine("Order # {0}, ordered on {1}",
                                       order.OrderId.ToString(),
                                       order.OrderDate.ToShortDateString());
                    Console.WriteLine("SKU\tDescription\tQty\tPrice");
                    Console.WriteLine("---\t-----------\t---\t-----");
                    //要将OrderItem实体的Item和Order属性设置为virtual，否引用它们会错。
                    foreach (var oi in order.OrderItems)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", oi.Item.SKU,
                                           oi.Item.Description, oi.Count.ToString(),
                                            oi.Item.Price.HasValue?oi.Item.Price.Value.ToString("C"):"");
                    }
                }
            }

        }
    }
}
