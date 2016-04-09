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

namespace QueryingAnEntityDataModel.Recipe10
{
    /// <summary>
    /// 3-9过滤关联实体
    /// </summary>
    public class Recipe10Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                // 删除之前的测试数据
                context.Database.ExecuteSqlCommand("delete from chapter3.topsellings");
                context.Database.ExecuteSqlCommand("delete from chapter3.products");
                // 添加新的测试数据
                // 注意p1没有与之关联的TopSelling实体
                var p1 = new Product { Name = "Trailrunner Backpack" };
                var p2 = new Product
                {
                    Name = "Green River Tent",
                    TopSelling = new TopSelling { Rating = 3 }
                };
                var p3 = new Product
                {
                    Name = "Prairie Home Dutch Oven",
                    TopSelling = new TopSelling { Rating = 4 }
                };
                var p4 = new Product
                {
                    Name = "QuickFire Fire Starter",
                    TopSelling = new TopSelling { Rating = 2 }
                };
                context.Products.Add(p1);
                context.Products.Add(p2);
                context.Products.Add(p3);
                context.Products.Add(p4);
                context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                //var products = from p in context.Products
                //               orderby p.TopSelling.Rating descending
                //               select p;
                var products = context.Products.OrderByDescending(p => p.TopSelling.Rating);
                Console.WriteLine("All products, including those without ratings");

                foreach (var product in products)
                {
                    Console.WriteLine("\t{0} [rating: {1}]", product.Name,
                        product.TopSelling == null ? "0"
                            : product.TopSelling.Rating.ToString());
                }
            }

            using (var context = new EFContext())
            {
                var products = from p in context.Products
                               join t in context.TopSellings on
                                  //注意，我们如何将结果集投影到另一个名为'g'的序列中，以及应用DefaultIfEmpty方法
                                  p.ProductId equals t.ProductId into g
                               from tps in g.DefaultIfEmpty()
                               orderby tps.Rating descending
                               select new
                               {
                                   Name = p.Name,
                                   Rating = tps.Rating == null ? 0 : tps.Rating
                               };

                Console.WriteLine("\nAll products, including those without ratings");
                foreach (var product in products)
                {
                    Console.WriteLine("\t{0} [rating: {1}]", product.Name,
                        product.Rating.ToString());
                }
            }

            using (var context = new EFContext())
            {
                var esql = @"select value p from products as p
                 order by case when p.TopSelling is null then 0
                                    else p.TopSelling.Rating end desc";
                var products = ((IObjectContextAdapter)context).ObjectContext.CreateQuery<Product>(esql);
                Console.WriteLine("\nAll products, including those without ratings");
                foreach (var product in products)
                {
                    Console.WriteLine("\t{0} [rating: {1}]", product.Name,
                        product.TopSelling == null ? "0"
                            : product.TopSelling.Rating.ToString());
                }
            }
        }

    }
}
