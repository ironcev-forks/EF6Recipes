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

namespace LoadingEntitiesAndNavigationProperties.Recipe13
{
    /// <summary>
    ///5-13  过滤预先加载的实体集合
    /// </summary>
    public class Recipe13Program
    {
        public static void Run()
        {
            using (var context = new EFContext())
            {
                //var cat1 = new Category { Name = "Science Fiction", ReleaseType = "DVD" };
                //var cat2 = new Category { Name = "Thriller", ReleaseType = "Blu-Ray" };
                //var movie1 = new Movie { Name = "Return to the Moon", Category = cat1, Rating = "PG-13" };
                //var movie2 = new Movie { Name = "Street Smarts", Category = cat2, Rating = "PG-13" };
                //var movie3 = new Movie { Name = "Alien Revenge", Category = cat1, Rating = "R" };
                //var movie4 = new Movie { Name = "Saturday Nights", Category = cat1, Rating = "PG-13" };
                //context.Categories.Add(cat1);
                //context.Categories.Add(cat2);
                //context.Movies.Add(movie1);
                //context.Movies.Add(movie2);
                //context.Movies.Add(movie3);
                //context.Movies.Add(movie4);
                //context.SaveChanges();
            }

            using (var context = new EFContext())
            {
                // 通过ReleaseType和Rating过虑
                // 创建匿名类型集合
                //这个方法凭借匿名对象帮助我们绕开了预先加载中的限制，预先加载不允许我们过滤预先加载的实体集合。
                //注意，正如前面小节中的示例演示的那样，当我们显式加载时，我们能过虑一个预先加载的实体集合。
                //记住，匿名类型对象的生命周期范围只在创建它的方法中，方法不能返回匿名类型。
                //如果我们目标是返回一个应用后面要处理的实体集，那么我们需要创建一个确切的类型来存放数据，然后将它返回
                var cats = from c in context.Categories
                           where c.ReleaseType == "DVD"
                           select new
                           {
                               category = c,
                               movies = c.Movies.Where(m => m.Rating == "PG-13")
                           };

                Console.WriteLine("PG-13 Movies Released on DVD");
                Console.WriteLine("============================");
                foreach (var cat in cats)
                {
                    var category = cat.category;
                    Console.WriteLine("Category: {0}", category.Name);
                    foreach (var movie in cat.movies)
                    {
                        Console.WriteLine("\tMovie: {0}", movie.Name);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
